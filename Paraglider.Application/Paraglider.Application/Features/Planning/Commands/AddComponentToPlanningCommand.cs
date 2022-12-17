using MediatR;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.NoSQL.Entities;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Attributes;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.Infrastructure.Common.MongoDB;
using Paraglider.Infrastructure.Common.Response;
using System.ComponentModel.DataAnnotations;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.Application.Features.Planning.Commands;

public class AddComponentToPlanningRequest : IRequest<OperationResult>
{
    [Required] 
    public string ComponentId { get; set; } = null!;
}

public class AddComponentToPlanningCommandHandler 
    : IRequestHandler<AddComponentToPlanningRequest, OperationResult>
{
    private readonly IComponentAdditionHistoryRepository _componentAdditionHistoryRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMongoDataAccess<Component> _components;
    private readonly IHttpContextAccessor _accessor;

    public AddComponentToPlanningCommandHandler(
        IComponentAdditionHistoryRepository componentAdditionHistoryRepository,
        IMongoDataAccess<Component> components,
        IUserRepository userRepository,
        IHttpContextAccessor accessor)
    {
        _componentAdditionHistoryRepository = componentAdditionHistoryRepository;
        _components = components;
        _userRepository = userRepository;
        _accessor = accessor;
    }

    public async Task<OperationResult> Handle(
        AddComponentToPlanningRequest request,
        CancellationToken cancellationToken)
    {
        var operation = new OperationResult();

        //валидируем полученные данные
        var validation = AttributeValidator.Validate(request);
        if (!validation.IsValid()) return operation.AddError(validation);

        //проверяем, что такой компонент существует
        var component = await _components.FindByIdAsync(request.ComponentId);
        if (component is null)
        {
            return operation.AddError(ExceptionMessages.ObjectNotFound(nameof(Component)));
        }

        //получаем текущего пользователя
        var username = _accessor.HttpContext!.User.Identity!.Name;
        var user = await _userRepository.FindByUsernameAsync(username!);
        if (user is null)
        {
            return operation.AddError(ExceptionMessages.ObjectNotFound(nameof(ApplicationUser)));
        }

        //проверяем, что категория добавляемого компонента выбрана пользователем
        var categoryId = component.Category.Id;
        if (!user.Planning.Categories.Any(x => x.Id == categoryId))
        {
            return operation.AddError("У пользователя не выбрана такая категория.");
        }

        //проверяем, что у пользователя еще нет такого компонента в плане
        if (user.Planning.PlanningComponents.Any(x => x.ComponentId == component.Id))
        {
            return operation.AddError("Этот компонент уже находится в плане пользователя.");
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
            await _componentAdditionHistoryRepository.AddAsync(new ComponentAdditionHistory()
            {
                UserId = user.Id,
                ComponentId = component.Id
            });
            await _componentAdditionHistoryRepository.SaveChangesAsync();

            return operation.AddSuccess(Messages.ObjectUpdated(nameof(ApplicationUser)));
        }
        catch (Exception exception)
        {
            return operation.AddError(exception.Message, exception);
        }
    }
}