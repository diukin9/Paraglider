using MediatR;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.Infrastructure.Common.Response;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Paraglider.Application.Features.Categories.Commands;

public class AddCategoryToUserRequest : IRequest<InternalOperation>
{
    [Required]
    [JsonPropertyName("category_id")]
    public Guid CategoryId { get; set; }
}

public class AddCategoryToUserCommandHandler
    : IRequestHandler<AddCategoryToUserRequest, InternalOperation>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _accessor;

    public AddCategoryToUserCommandHandler(
        ICategoryRepository categoryRepository,
        IUserRepository userRepository,
        IHttpContextAccessor accessor)
    {
        _categoryRepository = categoryRepository;
        _userRepository = userRepository;
        _accessor = accessor;
    }

    public async Task<InternalOperation> Handle(
        AddCategoryToUserRequest request,
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

        //проверяем, что такая категория существует
        var category = await _categoryRepository.FindByIdAsync(request.CategoryId);
        if (category is null) return operation.AddError("Категория не найдена");

        //проверяем, что у пользователя отсутствует добавляемая категория
        if (user.Planning.Categories.Any(x => x.Id == category.Id))
        {
            return operation.AddError("Категория уже добавлена в план пользователя");
        }

        //добавляем категорию пользователю
        user.Planning.Categories.Add(category);

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