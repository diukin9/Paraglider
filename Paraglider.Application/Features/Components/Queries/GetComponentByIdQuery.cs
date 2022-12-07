using MediatR;
using Paraglider.Domain.NoSQL.Entities;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Attributes;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.Infrastructure.Common.MongoDB;
using System.ComponentModel.DataAnnotations;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.API.Features.Components.Queries;

public class GetComponentByIdRequest : IRequest<OperationResult<object>>
{
    [Required, NotEmptyGuid] public Guid Id { get; set; }

    public GetComponentByIdRequest(Guid id)
    {
        Id = id;
    }
}

public class GetComponentByIdQueryHandler 
    : IRequestHandler<GetComponentByIdRequest, OperationResult<object>>
{
    private readonly IMongoDataAccess<Component> _components;

    public GetComponentByIdQueryHandler(
        IMongoDataAccess<Component> components)
    {
        _components = components;
    }

    public async Task<OperationResult<object>> Handle(
        GetComponentByIdRequest request, 
        CancellationToken cancellationToken)
    {
        var operation = new OperationResult<object>();

        //валидируем полученные данные
        var validation = AttributeValidator.Validate(request);
        if (!validation.IsValid()) return operation.AddError(validation);

        //проверяем, что такой компонент существует
        var component = await _components.FindByIdAsync(request.Id);
        if (component is null)
        {
            return operation.AddError(ExceptionMessages.ObjectNotFound(nameof(Component)));
        }

        return operation.AddSuccess(string.Empty, component);
    }
}
