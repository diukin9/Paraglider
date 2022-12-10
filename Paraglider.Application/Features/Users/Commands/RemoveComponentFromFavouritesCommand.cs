using MediatR;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.NoSQL.Entities;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.Infrastructure.Common.MongoDB;
using System.ComponentModel.DataAnnotations;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.API.Features.Users.Commands;

public class RemoveComponentFromFavouritesRequest : IRequest<OperationResult>
{
    [Required] public string ComponentId { get; set; } = null!;
}

public class RemoveComponentFromFavouritesCommandHandler 
    : IRequestHandler<RemoveComponentFromFavouritesRequest, OperationResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IMongoDataAccess<Component> _components;
    private readonly IHttpContextAccessor _accessor;

    public RemoveComponentFromFavouritesCommandHandler(
        IMongoDataAccess<Component> components,
        IUserRepository userRepository,
        IHttpContextAccessor accessor)
    {
        _components = components;
        _userRepository = userRepository;
        _accessor = accessor;
    }

    public async Task<OperationResult> Handle(
        RemoveComponentFromFavouritesRequest request,
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

        var removable = user.Favourites
            .Where(x => x.ComponentId == request.ComponentId)
            .SingleOrDefault();

        //проверяем, что такой компонент находится в избранном у пользователя
        if (removable is null)
        {
            return operation.AddError("Пользователь не добавлял такой компонент в избранное.");
        }

        //удаляем компонент из избранного
        user.Favourites.Remove(removable);

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