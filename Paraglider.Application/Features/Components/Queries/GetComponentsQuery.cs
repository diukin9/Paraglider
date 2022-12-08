using MapsterMapper;
using MediatR;
using Paraglider.API.DataTransferObjects;
using Paraglider.Domain.NoSQL.Entities;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Attributes;
using Paraglider.Infrastructure.Common.Enums;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.Infrastructure.Common.MongoDB;
using System.ComponentModel.DataAnnotations;

namespace Paraglider.API.Features.Components.Queries;

public class GetComponentsRequest : IRequest<OperationResult<IEnumerable<ComponentDTO>>>
{
    [Required, NotEmptyGuid] public Guid CategoryId { get; set; } //TODO сделать верхнюю границу для пагинации
    [NotNegative] public int? PerPage { get; set; } = 15;
    [NotNegative] public int? Page { get; set; } = 1;
    [IsSortingKey(typeof(ComponentSorterKey))] public string? SortingKey { get; set; }
}

public class GetComponentsQueryHandler 
    : IRequestHandler<GetComponentsRequest, 
        OperationResult<IEnumerable<ComponentDTO>>>
{
    private readonly IMongoDataAccess<Component> _components;
    private readonly IMapper _mapper;

    public GetComponentsQueryHandler(
        IMongoDataAccess<Component> components,
        IMapper mapper)
    {
        _components = components;
        _mapper = mapper;
    }

    public async Task<OperationResult<IEnumerable<ComponentDTO>>> Handle(
        GetComponentsRequest request, 
        CancellationToken cancellationToken)
    {
        var operation = new OperationResult<IEnumerable<ComponentDTO>>();

        //валидируем полученные данные
        var validation = AttributeValidator.Validate(request);
        if (!validation.IsValid()) return operation.AddError(validation);

        //получаем компоненты
        var components = await _components.FindAsync(
            filter: x => x.Category.Id == request.CategoryId,
            // TODO доделать сортировку sort: x => ...
            skip: request.PerPage!.Value * request.Page!.Value - request.PerPage!.Value,
            limit: request.PerPage.Value);

        var model = _mapper.Map<IEnumerable<ComponentDTO>>(components);

        return operation.AddSuccess(string.Empty, model);
    }
}

