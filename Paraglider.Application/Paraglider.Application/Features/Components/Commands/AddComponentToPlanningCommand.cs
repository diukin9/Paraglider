using MediatR;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.Infrastructure.Common.Response;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Paraglider.Application.Features.Planning.Commands;

public class AddComponentToPlanningRequest : IRequest<InternalOperation>
{
    [Required]
    [JsonPropertyName("component_id")]
    public Guid ComponentId { get; set; }
}

public class AddComponentToPlanningCommandHandler
    : IRequestHandler<AddComponentToPlanningRequest, InternalOperation>
{
    private readonly IComponentAddHistoryRepository _componentAdditionHistoryRepository;
    private readonly IUserRepository _userRepository;
    private readonly IComponentRepository _componentRepository;
    private readonly IHttpContextAccessor _accessor;

    public AddComponentToPlanningCommandHandler(
        IComponentAddHistoryRepository componentAdditionHistoryRepository,
        IComponentRepository componentRepository,
        IUserRepository userRepository,
        IHttpContextAccessor accessor)
    {
        _componentAdditionHistoryRepository = componentAdditionHistoryRepository;
        _componentRepository = componentRepository;
        _userRepository = userRepository;
        _accessor = accessor;
    }

    public async Task<InternalOperation> Handle(
        AddComponentToPlanningRequest request,
        CancellationToken cancellationToken)
    {
        var operation = new InternalOperation();

        //валидируем полученные данные
        var validation = AttributeValidator.Validate(request);
        if (!validation.IsValid()) return operation.AddError(validation);

        //проверяем, что такой компонент существует
        var component = await _componentRepository.FindByIdAsync(request.ComponentId);
        if (component is null) return operation.AddError("Компонент не найден");

        //получаем текущего пользователя
        var nameIdentifier = _accessor.HttpContext!.GetNameIdentifier();
        var user = await _userRepository.FindByNameIdentifierAsync(nameIdentifier!);
        if (user is null) return operation.AddError("Пользователь не найден");

        //проверяем, что категория добавляемого компонента выбрана пользователем
        var categoryId = component.Category.Id;
        if (!user.Planning.Categories.Any(x => x.Id == categoryId))
        {
            return operation.AddError("Категория отсутствует в плане пользователя");
        }

        //проверяем, что у пользователя еще нет такого компонента в плане
        if (user.Planning.PlanningComponents.Any(x => x.ComponentId == component.Id))
        {
            return operation.AddError("Компонент уже находится в плане пользователя");
        }

        //добавляем компонент
        user.Planning.PlanningComponents.Add(new PlanningComponent()
        {
            CategoryId = categoryId,
            ComponentId = component.Id,
            ComponentDesc = new ComponentDesc()
        });

        try
        {
            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();

            //добавляем в историю
            await _componentAdditionHistoryRepository.AddAsync(new ComponentAddHistory()
            {
                UserId = user.Id,
                ComponentId = component.Id
            });
            await _componentAdditionHistoryRepository.SaveChangesAsync();

            return operation.AddSuccess();
        }
        catch (Exception exception)
        {
            return operation.AddError(exception.Message, exception);
        }
    }
}