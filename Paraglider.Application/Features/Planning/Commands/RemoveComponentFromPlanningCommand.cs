using FluentValidation;
using MediatR;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.NoSQL.Entities;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.Infrastructure.Common.MongoDB;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.API.Features.Planning.Commands;

public record RemoveComponentFromPlanningRequest(Guid ComponentId) : IRequest<OperationResult>;

public class RemoveComponentFromPlanningRequestValidator : AbstractValidator<RemoveComponentFromPlanningRequest>
{
    public RemoveComponentFromPlanningRequestValidator() => RuleSet(DefaultRuleSetName, () =>
    {
        RuleFor(x => x.ComponentId).NotEmpty();
    });
}

public class RemoveComponentFromPlanningCommandHandler 
    : IRequestHandler<RemoveComponentFromPlanningRequest, OperationResult>
{
    private readonly IValidator<RemoveComponentFromPlanningRequest> _validator;
    private readonly IUserRepository _userRepository;
    private readonly IMongoDataAccess<Component> _components;
    private readonly IHttpContextAccessor _accessor;

    public RemoveComponentFromPlanningCommandHandler(
        IMongoDataAccess<Component> components,
        IUserRepository userRepository,
        IHttpContextAccessor accessor,
        IValidator<RemoveComponentFromPlanningRequest> validator)
    {
        _components = components;
        _validator = validator;
        _userRepository = userRepository;
        _accessor = accessor;
    }

    public async Task<OperationResult> Handle(
        RemoveComponentFromPlanningRequest request,
        CancellationToken cancellationToken)
    {
        var operation = new OperationResult();

        //валидируем полученные данные
        var validateResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validateResult.IsValid)
        {
            return operation.AddError(string.Join("; ", validateResult.Errors));
        }

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
            return operation.AddError("The user does not have such a component");
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