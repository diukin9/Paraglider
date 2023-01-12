using MediatR;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.Infrastructure.Common.Response;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Paraglider.Application.Features.Categories.Commands;

public record RemoveCategoryFromUserRequest : IRequest<InternalOperation>
{
    [Required]
    [JsonPropertyName("category_id")]
    public Guid CategoryId { get; set; }
}

public class RemoveCategoryFromUserCommandHandler
    : IRequestHandler<RemoveCategoryFromUserRequest, InternalOperation>
{
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _accessor;

    public RemoveCategoryFromUserCommandHandler(
        IUserRepository userRepository,
        IHttpContextAccessor accessor)
    {
        _userRepository = userRepository;
        _accessor = accessor;
    }

    public async Task<InternalOperation> Handle(
        RemoveCategoryFromUserRequest request,
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

        //проверяем, что такая категория есть у пользователя
        var category = user.Planning.Categories
            .Where(x => x.Id == request.CategoryId)
            .SingleOrDefault();

        if (category is null)
        {
            return operation.AddError("Категория отсутствует в плане пользователя");
        }

        //удаляем все компоненты принадлежащие удаляемой категории
        var removable = user.Planning.PlanningComponents
            .Where(x => x.CategoryId == category.Id)
            .ToList();

        removable.ForEach(x => user.Planning.PlanningComponents.Remove(x));

        //удаляем категорию из плана пользователя
        user.Planning.Categories.Remove(category);

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