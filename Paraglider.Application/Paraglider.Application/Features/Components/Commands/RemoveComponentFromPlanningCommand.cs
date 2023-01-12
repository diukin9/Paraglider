using MediatR;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.Infrastructure.Common.Response;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Paraglider.Application.Features.Planning.Commands;

public record RemoveComponentFromPlanningRequest : IRequest<InternalOperation>
{
    [Required]
    [JsonPropertyName("component_id")]
    public Guid ComponentId { get; set; }
}

public class RemoveComponentFromPlanningCommandHandler
    : IRequestHandler<RemoveComponentFromPlanningRequest, InternalOperation>
{
    private readonly IUserRepository _userRepository;
    private readonly IComponentRepository _componentRepository;
    private readonly IHttpContextAccessor _accessor;

    public RemoveComponentFromPlanningCommandHandler(
        IComponentRepository componentRepository,
        IUserRepository userRepository,
        IHttpContextAccessor accessor)
    {
        _componentRepository = componentRepository;
        _userRepository = userRepository;
        _accessor = accessor;
    }

    public async Task<InternalOperation> Handle(
        RemoveComponentFromPlanningRequest request,
        CancellationToken cancellationToken)
    {
        var operation = new InternalOperation();

        //валидируем полученные данные
        var validation = AttributeValidator.Validate(request);
        if (!validation.IsValid()) return operation.AddError(validation);

        //получаем текущего пользователя
        var nameIdentifier = _accessor.HttpContext!.GetNameIdentifier();
        var user = await _userRepository.FindByNameIdentifierAsync(nameIdentifier!);
        if (user is null) return operation.AddError("Пользователь не найден");

        //проверяем, что такой компонент существует
        var removable = user.Planning.PlanningComponents
            .Where(x => x.ComponentId == request.ComponentId)
            .SingleOrDefault();

        if (removable is null) return operation.AddError("Компонент отсутствует в плане пользователя");

        //удаляем компонент 
        user.Planning.PlanningComponents.Remove(removable);

        try
        {
            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();
            return operation.AddSuccess();
        }
        catch (Exception exception)
        {
            return operation.AddError(exception.Message, exception);
        }
    }
}