﻿using FluentValidation;
using MediatR;
using Paraglider.Domain.NoSQL.Entities;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.MongoDB;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.API.Features.Components.Queries;

public record GetComponentByIdRequest(Guid Id) : IRequest<OperationResult<object>>;

public class GetComponentByIdRequestValidator : AbstractValidator<GetComponentByIdRequest>
{
    public GetComponentByIdRequestValidator() => RuleSet(DefaultRuleSetName, () =>
    {
        RuleFor(x => x.Id).NotEmpty();
    });
}

public class GetComponentByIdQueryHandler : IRequestHandler<GetComponentByIdRequest, OperationResult<object>>
{
    private readonly IMongoDataAccess<Component> _components;
    private readonly IValidator<GetComponentByIdRequest> _validator;

    public GetComponentByIdQueryHandler(
        IMongoDataAccess<Component> components, 
        IValidator<GetComponentByIdRequest> validator)
    {
        _components = components;
        _validator = validator;
    }

    public async Task<OperationResult<object>> Handle(GetComponentByIdRequest request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult<object>();

        //валидируем полученные данные
        var validateResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validateResult.IsValid)
        {
            return operation.AddError(validateResult.Errors);
        }

        //проверяем, что такой компонент существует
        var component = await _components.FindByIdAsync(request.Id);
        if (component is null)
        {
            return operation.AddError(ExceptionMessages.ObjectNotFound(nameof(Component)));
        }

        return operation.AddSuccess(string.Empty, component);
    }
}
