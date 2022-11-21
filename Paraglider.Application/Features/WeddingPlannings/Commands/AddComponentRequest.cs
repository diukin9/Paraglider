using FluentValidation;
using MediatR;
using Paraglider.API.Features.Authorization.Commands;
using Paraglider.Data.EntityFrameworkCore.Repositories;
using Paraglider.Domain.NoSQL.Entities;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Abstractions;
using Paraglider.Infrastructure.Common.Extensions;
using Reinforced.Typings.Attributes;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.API.Features.WeddingPlannings.Commands;

[TsClass]
public class AddComponentRequest : IRequest<OperationResult>
{
    public Guid ComponentId { get; set; }

    public AddComponentRequest(Guid componentId)
    {
        ComponentId = componentId;
    }
}

public class AddComponentRequestValidator : AbstractValidator<AddComponentRequest>
{
    public AddComponentRequestValidator() => RuleSet(DefaultRuleSetName, () =>
    {
        RuleFor(x => x.ComponentId).Must(x => x != Guid.Empty);
    });
}

public class AddComponentQueryHandler : IRequestHandler<AddComponentRequest, OperationResult>
{
    private readonly IDataAccess<WeddingComponent> _components;
    private readonly UserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _accessor;
    private readonly IValidator<AddComponentRequest> _validator;

    public AddComponentQueryHandler(
        IDataAccess<WeddingComponent> components,
        IUnitOfWork unitOfWork,
        IHttpContextAccessor accessor,
        IValidator<AddComponentRequest> validator)
    {
        _components = components;
        _unitOfWork = unitOfWork;
        _userRepository = (UserRepository)unitOfWork.GetRepository<ApplicationUser>();
        _accessor = accessor;
        _validator = validator;
    }

    public async Task<OperationResult> Handle(AddComponentRequest request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult();

        //валидируем полученные данные
        var validateResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validateResult.IsValid)
        {
            return operation.AddError(string.Join("; ", validateResult.Errors));
        }

        //проверить, что компонент существует
        if (await _components.GetByIdAsync(request.ComponentId) is null)
        {
            return operation.AddError(ExceptionMessages.ObjectNotFound(nameof(WeddingComponent)));
        }

        //получаем текущего пользователя
        var username = _accessor.HttpContext!.User!.Identity!.Name;
        var user = await _userRepository.FindByUsernameAsync(username!);

        //проверяем, что такого компонента еще нет в свадебном плане пользователя
        var isExist = user!.WeddingPlannings
            .Where(x => x.CityId == user.CityId)
            .Any(x => x.ComponentIds.Contains(request.ComponentId));

        if (isExist) return operation.AddError("Such a component has already been added by the user");

        //добавляем компонент
        user!.WeddingPlannings
            .Where(x => x.CityId == user.CityId)
            .Single()
            .ComponentIds.Add(request.ComponentId);

        try
        {
            _userRepository.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();
            return operation.AddSuccess(string.Empty);
        }
        catch (Exception exception)
        {
            return operation.AddError(exception.Message, exception);
        }
    }
}