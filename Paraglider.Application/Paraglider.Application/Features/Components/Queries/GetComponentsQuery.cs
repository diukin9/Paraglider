using MapsterMapper;
using MediatR;
using Paraglider.Application.DataTransferObjects;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Attributes;
using Paraglider.Infrastructure.Common.Enums;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.Infrastructure.Common.Response;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Paraglider.Application.Features.Components.Queries;

public class GetComponentsRequest : IRequest<InternalOperation<IEnumerable<ComponentDTO>>>
{
    [Required]
    [JsonPropertyName("category_id")]
    public Guid CategoryId { get; set; }

    [Required]
    [JsonPropertyName("city_id")]
    public Guid CityId { get; set; }

    [Required, Range(1, 100)]
    [JsonPropertyName("per_page")]
    public int? PerPage { get; set; } = 15;

    [Required, NotNegative]
    [JsonPropertyName("page")]
    public int? Page { get; set; } = 1;

    [Required, IsEnumName(typeof(ComponentSorterKey))]
    [JsonPropertyName("sorting_key")]
    public string SortingKey { get; set; } = null!;

    [Required]
    [JsonPropertyName("descending")]
    public bool Descending { get; set; } = false;
}

public class GetComponentsQueryHandler
    : IRequestHandler<GetComponentsRequest,
        InternalOperation<IEnumerable<ComponentDTO>>>
{
    private readonly IMapper _mapper;
    private readonly IComponentRepository _componentRepository;

    public GetComponentsQueryHandler(
        IComponentRepository componentRepository,
        IMapper mapper)
    {
        _componentRepository = componentRepository;
        _mapper = mapper;
    }

    public async Task<InternalOperation<IEnumerable<ComponentDTO>>> Handle(
        GetComponentsRequest request,
        CancellationToken cancellationToken)
    {
        var operation = new InternalOperation<IEnumerable<ComponentDTO>>();

        //валидируем полученные данные
        var validation = AttributeValidator.Validate(request);
        if (!validation.IsValid()) return operation.AddError(validation);

        //получаем компоненты
        var components = await _componentRepository.FindAsync(
            filter: x => x.Category.Id == request.CategoryId && x.City.Id == request.CityId,
            orderBy: Enum.Parse(typeof(ComponentSorterKey), request.SortingKey) switch
            {
                ComponentSorterKey.Popularity => x => x.Popularity,
                ComponentSorterKey.Rating => x => x.Rating,
                _ => throw new ApplicationException()
            },
            isAscending: !request.Descending,
            skip: request.PerPage!.Value * request.Page!.Value - request.PerPage!.Value,
            limit: request.PerPage.Value);

        var model = _mapper.Map<IEnumerable<ComponentDTO>>(components);

        return operation.AddSuccess(model);
    }
}

