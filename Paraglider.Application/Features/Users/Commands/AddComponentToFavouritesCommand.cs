using FluentValidation;
using MediatR;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.NoSQL.Entities;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.MongoDB;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.API.Features.Users.Commands;

public record AddComponentToFavouritesRequest(Guid ComponentId) : IRequest<OperationResult>;

public class AddComponentRequestValidator : AbstractValidator<AddComponentToFavouritesRequest>
{
    public AddComponentRequestValidator() => RuleSet(DefaultRuleSetName, () =>
    {
        RuleFor(x => x.ComponentId).NotEmpty();
    });
}

public class AddComponentToFavouritesCommandHandler 
    : IRequestHandler<AddComponentToFavouritesRequest, OperationResult>
{
    private readonly IValidator<AddComponentToFavouritesRequest> _validator;
    private readonly IUserRepository _userRepository;
    private readonly IMongoDataAccess<Component> _components;
    private readonly IHttpContextAccessor _accessor;

    public AddComponentToFavouritesCommandHandler(
        IMongoDataAccess<Component> components,
        IUserRepository userRepository,
        IHttpContextAccessor accessor,
        IValidator<AddComponentToFavouritesRequest> validator)
    {
        _components = components;
        _validator = validator;
        _userRepository = userRepository;
        _accessor = accessor;
    }

    public async Task<OperationResult> Handle(
        AddComponentToFavouritesRequest request,
        CancellationToken cancellationToken)
    {
        var operation = new OperationResult();

        //валидируем полученные данные
        var validateResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validateResult.IsValid)
        {
            return operation.AddError(validateResult.Errors);
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

        //проверяем, что у пользователя этот компонент еще не добавлен в избранное
        var componentId = component[MongoIdName];
        if (user.Favourites.Any(x => x.Id == component))
        {
            return operation.AddError("This component is already in the user's favorites");
        }

        //добавляем компонент в избранное
        user.Favourites.Add(new UserComponent() { ComponentId = componentId });

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