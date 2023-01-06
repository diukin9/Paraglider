using MediatR;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Attributes;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.Infrastructure.Common.Response;
using System.ComponentModel.DataAnnotations;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.Application.Features.Planning.Commands;

public class AddCategoryToUserRequest : IRequest<OperationResult>
{
    [Required, NotEmptyGuid] 
    public Guid CategoryId { get; set; }
}

public class AddCategoryToUserCommandHandler 
    : IRequestHandler<AddCategoryToUserRequest, OperationResult>
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

    public async Task<OperationResult> Handle(
        AddCategoryToUserRequest request, 
        CancellationToken cancellationToken)
    {
        var operation = new OperationResult();

        //валидируем полученные данные
        var validation = AttributeValidator.Validate(request);
        if (!validation.IsValid()) return operation.AddError(validation);

        //получаем текущего пользователя
        var username = _accessor.HttpContext!.User!.Identity!.Name;
        var user = await _userRepository.FindByUsernameAsync(username!);
        if (user is null)
        {
            return operation.AddError(ExceptionMessages.ObjectNotFound(nameof(ApplicationUser)));
        }

        //проверяем, что такая категория существует
        var category = await _categoryRepository.FindByIdAsync(request.CategoryId);
        if (category is null)
        {
            return operation.AddError(ExceptionMessages.ObjectNotFound(nameof(Category)));
        }

        //проверяем, что у пользователя отсутствует добавляемая категория
        if (user.Planning.Categories.Any(x => x.Id == category.Id))
        {
            return operation.AddWarning("Данная категория уже была выбрана пользователем");
        }

        //добавляем категорию пользователю
        user.Planning.Categories.Add(category);

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