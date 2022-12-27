using System.Security.Claims;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Paraglider.Application.Extensions;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Domain.RDB.Enums;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.Infrastructure.Common.Response;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.Application.Features.Users.Queries;

public record CheckUserExistenceByExternalLoginInfoRequest : IRequest<OperationResult<bool>>;

public class CheckUserExistenceByExternalLoginInfoQueryHandler 
    : IRequestHandler<CheckUserExistenceByExternalLoginInfoRequest, OperationResult<bool>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IMapper _mapper;

    public CheckUserExistenceByExternalLoginInfoQueryHandler(
        UserManager<ApplicationUser> userManager,
        IMapper mapper,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _mapper = mapper;
        _signInManager = signInManager;
    }

    public async Task<OperationResult<bool>> Handle(
        CheckUserExistenceByExternalLoginInfoRequest request, 
        CancellationToken cancellationToken)
    {
        var operation = new OperationResult<bool>();

        //получаем ExternalLoginInfo
        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info is null)
        {
            return operation.AddError(ExceptionMessages.ObjectNotFound(nameof(ExternalLoginInfo)));
        }

        //получаем провайдера и внешний id
        var provider = Enum.Parse<AuthProvider>(info.LoginProvider);
        var externalId = info.Principal.Claims.GetByClaimType(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(externalId))
        {
            return operation.AddError(ExceptionMessages.ValueNullOrEmpty(nameof(externalId)));
        }

        //получаем пользователя
        var user = await _userManager.FindByExternalLoginInfoAsync(provider, externalId);
        return operation.AddSuccess(string.Empty, user is not null);
    }
}
