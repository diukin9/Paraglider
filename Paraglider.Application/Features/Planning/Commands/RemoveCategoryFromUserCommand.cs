using FluentValidation;
using MediatR;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.Infrastructure.Common.Response;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.API.Features.Planning.Commands;

public record RemoveCategoryFromUserRequest(Guid CategoryId) : IRequest<OperationResult>;

public class RemoveCategoryFromUserRequestValidator : AbstractValidator<RemoveCategoryFromUserRequest>
{
    public RemoveCategoryFromUserRequestValidator() => RuleSet(DefaultRuleSetName, () =>
    {
        RuleFor(x => x.CategoryId).NotEmpty();
    });
}

public class RemoveCategoryFromUserCommandHandler : IRequestHandler<RemoveCategoryFromUserRequest, OperationResult>
{
    private readonly IValidator<RemoveCategoryFromUserRequest> _validator;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _accessor;

    public RemoveCategoryFromUserCommandHandler(
        ICategoryRepository categoryRepository,
        IUserRepository userRepository,
        IHttpContextAccessor accessor,
        IValidator<RemoveCategoryFromUserRequest> validator)
    {
        _categoryRepository = categoryRepository;
        _userRepository = userRepository;
        _accessor = accessor;
        _validator = validator;
    }

    public async Task<OperationResult> Handle(RemoveCategoryFromUserRequest request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult();

        //валидируем полученные данные
        var validateResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validateResult.IsValid)
        {
            return operation.AddError(string.Join("; ", validateResult.Errors));
        }

        //получаем текущего пользователя
        var username = _accessor.HttpContext!.User!.Identity!.Name;
        var user = await _userRepository.FindByUsernameAsync(username!);
        if (user is null)
        {
            return operation.AddError(ExceptionMessages.ObjectNotFound(nameof(ApplicationUser)));
        }

        //проверяем, что такая категория есть у пользователя
        var category = user.Planning.Categories
            .Where(x => x.Id == request.CategoryId)
            .SingleOrDefault();

        if (category is null)
        {
            return operation.AddError("The user has no such category");
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
            return operation.AddSuccess(Messages.ObjectUpdated(nameof(ApplicationUser)));
        }
        catch (Exception exception)
        {
            return operation.AddError(exception.Message, exception);
        }
    }
}