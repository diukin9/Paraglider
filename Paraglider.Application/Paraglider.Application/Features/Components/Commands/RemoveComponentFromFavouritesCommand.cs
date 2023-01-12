using MediatR;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.Infrastructure.Common.Response;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Paraglider.Application.Features.Components.Commands;

public class RemoveComponentFromFavouritesRequest : IRequest<InternalOperation>
{
    [Required]
    [JsonPropertyName("component_id")]
    public Guid ComponentId { get; set; }
}

public class RemoveComponentFromFavouritesCommandHandler
    : IRequestHandler<RemoveComponentFromFavouritesRequest, InternalOperation>
{
    private readonly IUserRepository _userRepository;
    private readonly IComponentRepository _componentRepository;
    private readonly IHttpContextAccessor _accessor;

    public RemoveComponentFromFavouritesCommandHandler(
        IComponentRepository componentRepository,
        IUserRepository userRepository,
        IHttpContextAccessor accessor)
    {
        _componentRepository = componentRepository;
        _userRepository = userRepository;
        _accessor = accessor;
    }

    public async Task<InternalOperation> Handle(
        RemoveComponentFromFavouritesRequest request,
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

        var removable = user.Favourites
            .Where(x => x.Id == request.ComponentId)
            .SingleOrDefault();

        //проверяем, что такой компонент находится в избранном у пользователя
        if (removable is null) return operation.AddError("Компонент отсутствует в избранном");

        //удаляем компонент из избранного
        user.Favourites.Remove(removable);

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