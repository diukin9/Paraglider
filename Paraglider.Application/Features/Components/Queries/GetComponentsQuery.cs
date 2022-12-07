using MediatR;
using Paraglider.Domain.NoSQL.Entities;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Attributes;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.Infrastructure.Common.MongoDB;
using System.ComponentModel.DataAnnotations;

namespace Paraglider.API.Features.Components.Queries;

public class GetComponentsRequest : IRequest<OperationResult<IEnumerable<object>>>
{
    [Required, NotEmptyGuid] public Guid CategoryId { get; set; }
    [NotNegative] public int PerPage { get; set; }
    [NotNegative] public int Page { get; set; }
    [IsSorterKey] public string? SorterKey { get; set; }

    public GetComponentsRequest(Guid categoryId, int perPage, int page, string? sorterKey = null)
    {
        CategoryId = categoryId;
        PerPage = perPage;
        Page = page;
        SorterKey = sorterKey;
    }
}

public class GetComponentsQueryHandler 
    : IRequestHandler<GetComponentsRequest, 
        OperationResult<IEnumerable<object>>>
{
    private readonly IMongoDataAccess<Component> _components;

    public GetComponentsQueryHandler(
        IMongoDataAccess<Component> components)
    {
        _components = components;
    }

    public async Task<OperationResult<IEnumerable<object>>> Handle(
        GetComponentsRequest request, 
        CancellationToken cancellationToken)
    {
        var operation = new OperationResult<IEnumerable<object>>();

        //валидируем полученные данные
        var validation = AttributeValidator.Validate(request);
        if (!validation.IsValid()) return operation.AddError(validation);

        //получаем компоненты
        var components = await _components.FindAsync(
            filter: x => x.Category.Id == request.CategoryId,
            // TODO доделать сортировку sort: x => ...
            skip: request.PerPage * request.Page - request.PerPage,
            limit: request.PerPage);

        return operation.AddSuccess(string.Empty, components);
    }
}

