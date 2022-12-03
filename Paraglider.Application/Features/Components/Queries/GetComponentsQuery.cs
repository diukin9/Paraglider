using FluentValidation;
using MediatR;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.NoSQL.Entities;
using Paraglider.Domain.NoSQL.ValueObjects;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Enums;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.Infrastructure.Common.MongoDB;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.API.Features.Components.Queries;

public record GetComponentsRequest(Guid CategoryId, int PerPage, int Page, string? SorterKey = null) 
    : IRequest<OperationResult>;

public class GetComponentsRequestValidator : AbstractValidator<GetComponentsRequest>
{
    public GetComponentsRequestValidator() => RuleSet(DefaultRuleSetName, () =>
    {
        RuleFor(x => x.CategoryId).NotEmpty();
        RuleFor(x => x.PerPage).GreaterThan(0);
        RuleFor(x => x.Page).GreaterThan(0);
        RuleFor(x => x.SorterKey).Custom((key, context) =>
        {
            var isEnumName = Enum
                .GetNames(typeof(ComponentSorterKey))
                .Contains(key, StringComparer.OrdinalIgnoreCase);

            if (key is not null && !isEnumName)
            {
                context.AddFailure("Passed a non-existent sorting key.");
            }
        });
    });
}

public class GetComponentsQueryHandler : IRequestHandler<GetComponentsRequest, OperationResult>
{
    private readonly IMongoDataAccess<Component> _components;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IValidator<GetComponentsRequest> _validator;

    public GetComponentsQueryHandler(
        IMongoDataAccess<Component> components,
        ICategoryRepository categoryRepository,
        IValidator<GetComponentsRequest> validator)
    {
        _categoryRepository = categoryRepository;
        _components = components;
        _validator = validator;
    }

    public async Task<OperationResult> Handle(GetComponentsRequest request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult();

        //валидируем полученные данные
        var validateResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validateResult.IsValid)
        {
            return operation.AddError(string.Join("; ", validateResult.Errors));
        }

        //проверяем, что такая категория существует
        if (await _categoryRepository.FindByIdAsync(request.CategoryId) is null)
        {
            return operation.AddError(ExceptionMessages.ObjectNotFound(nameof(Category)));
        }

        //получаем компоненты
        var components = await _components.FindAsync(
            filter: x => x.Category.Id == request.CategoryId,
            // TODO доделать сортировку sort: x => ...
            skip: request.PerPage * request.Page - request.PerPage,
            limit: request.PerPage);

        return operation.AddSuccess(string.Empty, components);
    }
}

