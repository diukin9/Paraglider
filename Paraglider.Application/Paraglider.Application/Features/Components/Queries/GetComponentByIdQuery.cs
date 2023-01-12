using MapsterMapper;
using MediatR;
using Paraglider.Application.DataTransferObjects;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.Infrastructure.Common.Response;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Paraglider.Application.Features.Components.Queries;

public class GetComponentByIdRequest : IRequest<InternalOperation<ComponentDTO>>
{
    [Required]
    [JsonPropertyName("component_id")]
    public Guid Id { get; set; }

    public GetComponentByIdRequest(Guid id)
    {
        Id = id;
    }
}

public class GetComponentByIdQueryHandler
    : IRequestHandler<GetComponentByIdRequest, InternalOperation<ComponentDTO>>
{
    private readonly IComponentRepository _componentRepository;
    private readonly IMapper _mapper;

    public GetComponentByIdQueryHandler(
        IComponentRepository componentRepository,
        IMapper mapper)
    {
        _componentRepository = componentRepository;
        _mapper = mapper;
    }

    public async Task<InternalOperation<ComponentDTO>> Handle(
        GetComponentByIdRequest request,
        CancellationToken cancellationToken)
    {
        var operation = new InternalOperation<ComponentDTO>();

        //валидируем полученные данные
        var validation = AttributeValidator.Validate(request);
        if (!validation.IsValid()) return operation.AddError(validation);

        //проверяем, что такой компонент существует
        var component = await _componentRepository.FindByIdAsync(request.Id);
        if (component is null) return operation.AddError("Компонент не найден");

        var model = _mapper.Map<ComponentDTO>(component);
        return operation.AddSuccess(model);
    }
}
