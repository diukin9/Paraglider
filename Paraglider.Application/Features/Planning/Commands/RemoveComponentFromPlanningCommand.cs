using MediatR;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.NoSQL.Entities;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.Infrastructure.Common.MongoDB;
using System.ComponentModel.DataAnnotations;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.API.Features.Planning.Commands;

public record RemoveComponentFromPlanningRequest: IRequest<OperationResult>
{
    [Required] public string ComponentId { get; set; } = null!;
}

public class RemoveComponentFromPlanningCommandHandler 
    : IRequestHandler<RemoveComponentFromPlanningRequest, OperationResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IMongoDataAccess<Component> _components;
    private readonly IHttpContextAccessor _accessor;

    public RemoveComponentFromPlanningCommandHandler(
        IMongoDataAccess<Component> components,
        IUserRepository userRepository,
        IHttpContextAccessor accessor)
    {
        _components = components;
        _userRepository = userRepository;
        _accessor = accessor;
    }

    public async Task<OperationResult> Handle(
        RemoveComponentFromPlanningRequest request,
        CancellationToken cancellationToken)
    {
        var operation = new OperationResult();

        //валидируем полученные данные
        var validation = AttributeValidator.Validate(request);
        if (!validation.IsValid()) return operation.AddError(validation);

        //получаем текущего пользователя
        var username = _accessor.HttpContext!.User.Identity!.Name;
        var user = await _userRepository.FindByUsernameAsync(username!);
        if (user is null)
        {
            return operation.AddError(ExceptionMessages.ObjectNotFound(nameof(ApplicationUser)));
        }

        //проверяем, что такой компонент существует
        var removable = user.Planning.PlanningComponents
            .Where(x => x.ComponentId == request.ComponentId)
            .SingleOrDefault();

        if (removable is null)
        {
            return operation.AddError("Пользователь не имеет такого компонента в плане.");
        }

        //удаляем компонент 
        user.Planning.PlanningComponents.Remove(removable);

        try
        {
            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();
            return operation.AddSuccess(Messages.ObjectUpdated(nameof(ApplicationUser)));
        }
        catch (Exception exception)
        {
            return operation.AddError(exception.Message, exception);
        }
    }
}