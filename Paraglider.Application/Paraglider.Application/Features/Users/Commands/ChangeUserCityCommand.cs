using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Attributes;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.Infrastructure.Common.Response;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.Application.Features.Users.Commands;

public class ChangeUserCityRequest : IRequest<OperationResult>
{
    [Required, NotEmptyGuid] public Guid CityId { get; set; }
}

public class ChangeUserCityCommandHandler 
    : IRequestHandler<ChangeUserCityRequest, OperationResult>
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly HttpContext? _httpContext;

    public ChangeUserCityCommandHandler(IUserRepository userRepository,
        IHttpContextAccessor httpContextAccessor,
        UserManager<ApplicationUser> userManager)
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _httpContext = httpContextAccessor?.HttpContext;
    }

    public async Task<OperationResult> Handle(
        ChangeUserCityRequest request, 
        CancellationToken cancellationToken)
    {
        var operation = new OperationResult();

        //валидируем полученные данные
        var validation = AttributeValidator.Validate(request);
        if (!validation.IsValid()) return operation.AddError(validation);

        if (_httpContext is null)
            return operation.AddError(ExceptionMessages.ObjectIsNull(nameof(HttpContext)));

        var user = await _userManager.GetUserAsync(_httpContext.User);
        if (user == null)
            return operation.AddError(ExceptionMessages.ObjectIsNull(nameof(user)));

        return await _userRepository.ChangeCity(user.Id, request.CityId, cancellationToken)
            ? operation.AddSuccess(Messages.ObjectUpdated(nameof(ApplicationUser),
                nameof(ApplicationUser.City)))
            : operation.AddError(ExceptionMessages.UpdateError(nameof(ApplicationUser)));
    }
}