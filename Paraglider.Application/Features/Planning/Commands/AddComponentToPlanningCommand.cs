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

public record AddComponentToPlanningRequest(Guid ComponentId) : IRequest<OperationResult>;

public class AddComponentToPlanningRequestValidator : AbstractValidator<AddComponentToPlanningRequest>
{
    public AddComponentToPlanningRequestValidator() => RuleSet(DefaultRuleSetName, () =>
    {
        RuleFor(x => x.ComponentId).NotEmpty();
    });
}

public class AddComponentToPlanningCommandHandler : IRequestHandler<AddComponentToPlanningRequest, OperationResult>
{
    private readonly IValidator<AddComponentToPlanningRequest> _validator;
    private readonly IUserRepository _userRepository;
    private readonly IMongoDataAccess<Component> _components;
    private readonly IHttpContextAccessor _accessor;

    public AddComponentToPlanningCommandHandler(
        IMongoDataAccess<Component> components,
        IUserRepository userRepository,
        IHttpContextAccessor accessor,
        IValidator<AddComponentToPlanningRequest> validator)
    {
        _components = components;
        _validator = validator;
        _userRepository = userRepository;
        _accessor = accessor;
    }

    public async Task<OperationResult> Handle(
        AddComponentToPlanningRequest request,
        CancellationToken cancellationToken)
    {
        var operation = new OperationResult();

        //валидируем полученные данные
        var validateResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validateResult.IsValid)
        {
            return operation.AddError(string.Join("; ", validateResult.Errors));
        }

        //проверяем, что такой компонент существует
        dynamic? component = await _components.FindByIdAsync(request.ComponentId);
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
        var categoryId = component[nameof(Component.Category).ToLower()][MongoIdName];
        if (!user.Planning.Categories.Any(x => x.Id == categoryId))
        {
            return operation.AddError("The user does not have this category selected");
        }

        //проверяем, что у пользователя еще нет такого компонента в плане
        var componentId = component[MongoIdName];
        if (user.Planning.PlanningComponents.Any(x => x.Id == componentId))
        {
            return operation.AddError("This component is already in the user's plan");
        }

        //добавляем компонент
        user.Planning.PlanningComponents.Add(new PlanningComponent()
        {
            CategoryId = categoryId,
            ComponentId = componentId,
            ComponentDesc = new ComponentDesc()
        });

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