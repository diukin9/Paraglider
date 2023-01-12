using MediatR;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.Infrastructure.Common.Response;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Paraglider.Application.Features.Components.Commands;

public class AddComponentToFavouritesRequest : IRequest<InternalOperation>
{
    [Required]
    [JsonPropertyName("component_id")]
    public Guid ComponentId { get; set; }
}

public class AddComponentToFavouritesCommandHandler
    : IRequestHandler<AddComponentToFavouritesRequest, InternalOperation>
{
    private readonly IUserRepository _userRepository;
    private readonly IComponentRepository _componentRepository;
    private readonly IHttpContextAccessor _accessor;

    public AddComponentToFavouritesCommandHandler(
        IComponentRepository componentRepository,
        IUserRepository userRepository,
        IHttpContextAccessor accessor)
    {
        _componentRepository = componentRepository;
        _userRepository = userRepository;
        _accessor = accessor;
    }

    public async Task<InternalOperation> Handle(
        AddComponentToFavouritesRequest request,
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

        //проверяем, что у пользователя этот компонент еще не добавлен в избранное
        if (user.Favourites.Any(x => x.Id == component.Id))
        {
            return operation.AddError("Компонент уже находится в избранном");
        }

        //добавляем компонент в избранное
        user.Favourites.Add(component);

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