using MapsterMapper;
using MediatR;
using Paraglider.API.DataTransferObjects;
using Paraglider.Domain.NoSQL.Entities;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Attributes;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.Infrastructure.Common.MongoDB;
using System.ComponentModel.DataAnnotations;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.API.Features.Components.Queries;

public class GetComponentByIdRequest : IRequest<OperationResult<ComponentDTO>>
{
    [Required] public string Id { get; set; } = null!;

    public GetComponentByIdRequest(string id)
    {
        Id = id;
    }
}

public class GetComponentByIdQueryHandler 
    : IRequestHandler<GetComponentByIdRequest, OperationResult<ComponentDTO>>
{
    private readonly IMongoDataAccess<Component> _components;
    private readonly IMapper _mapper;

    public GetComponentByIdQueryHandler(
        IMongoDataAccess<Component> components,
        IMapper mapper)
    {
        _components = components;
        _mapper = mapper;
    }

    public async Task<OperationResult<ComponentDTO>> Handle(
        GetComponentByIdRequest request, 
        CancellationToken cancellationToken)
    {
        var operation = new OperationResult<ComponentDTO>();

        //валидируем полученные данные
        var validation = AttributeValidator.Validate(request);
        if (!validation.IsValid()) return operation.AddError(validation);

        //проверяем, что такой компонент существует
        var component = await _components.FindByIdAsync(request.Id);
        if (component is null)
        {
            return operation.AddError(ExceptionMessages.ObjectNotFound(nameof(Component)));
        }

        var model = _mapper.Map<ComponentDTO>(component);
        return operation.AddSuccess(string.Empty, model);
    }
}
