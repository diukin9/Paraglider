using FluentValidation;
using MapsterMapper;
using MediatR;
using Paraglider.API.Extensions;
using Paraglider.Domain.NoSQL.Entities;
using Paraglider.Domain.NoSQL.Enums;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Abstractions;
using Paraglider.Infrastructure.Common.Extensions;
using Reinforced.Typings.Attributes;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.API.Features.WeddingComponents.Queries;

[TsClass]
public class GetComponentByIdRequest : IRequest<OperationResult>
{
    public Guid Id { get; set; }

    public GetComponentByIdRequest(Guid id)
    {
        Id = id;
    }
}

public class GetComponentByIdRequestValidator : AbstractValidator<GetComponentByIdRequest>
{
    public GetComponentByIdRequestValidator() => RuleSet(DefaultRuleSetName, () =>
    {
        RuleFor(x => x.Id).Must(x => x != Guid.Empty);
    });
}

public class GetByIdQueryHandler : IRequestHandler<GetComponentByIdRequest, OperationResult>
{
    private readonly IDataAccess<WeddingComponent> _dataAccess;
    private readonly IValidator<GetComponentByIdRequest> _validator;
    private readonly IMapper _mapper;

    public GetByIdQueryHandler(
        IDataAccess<WeddingComponent> dataAccess,
        IValidator<GetComponentByIdRequest> validator,
        IMapper mapper)
    {
        _dataAccess = dataAccess;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<OperationResult> Handle(GetComponentByIdRequest request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult();

        var validateResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validateResult.IsValid)
        {
            return operation.AddError(string.Join("; ", validateResult.Errors));
        }

        //получаем компонент из mongoDB
        var component = await _dataAccess.GetByIdAsync(request.Id);
        if (component is null)
        {
            return operation.AddWarning(ExceptionMessages.ObjectNotFound(nameof(component)));
        }

        //конвертируем компанент
        var result = component.Convert(_mapper);

        return operation.AddSuccess(string.Empty, result);
    }
}
