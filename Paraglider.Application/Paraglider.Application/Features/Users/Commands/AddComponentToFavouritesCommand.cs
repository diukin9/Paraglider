using MediatR;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.NoSQL.Entities;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.Infrastructure.Common.MongoDB;
using Paraglider.Infrastructure.Common.Response;
using System.ComponentModel.DataAnnotations;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.Application.Features.Users.Commands;

public class AddComponentToFavouritesRequest : IRequest<OperationResult>
{
    [Required] 
    public string ComponentId { get; set; } = null!;
}

public class AddComponentToFavouritesCommandHandler 
    : IRequestHandler<AddComponentToFavouritesRequest, OperationResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IMongoDataAccess<Component> _components;
    private readonly IHttpContextAccessor _accessor;

    public AddComponentToFavouritesCommandHandler(
        IMongoDataAccess<Component> components,
        IUserRepository userRepository,
        IHttpContextAccessor accessor)
    {
        _components = components;
        _userRepository = userRepository;
        _accessor = accessor;
    }

    public async Task<OperationResult> Handle(
        AddComponentToFavouritesRequest request,
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
        var identifier = _accessor.HttpContext!.Request.Headers.GetNameIdentifierFromBearerToken();
        var user = await _userRepository.FindByNameIdentifierAsync(identifier!);
        if (user is null)
        {
            return operation.AddError(ExceptionMessages.ObjectNotFound(nameof(ApplicationUser)));
        }

        //проверяем, что у пользователя этот компонент еще не добавлен в избранное
        if (user.Favourites.Any(x => x.ComponentId == component.Id))
        {
            return operation.AddError("Этот компонент уже находится в избранном.");
        }

        //добавляем компонент в избранное
        user.Favourites.Add(new UserComponent() { ComponentId = component.Id });

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