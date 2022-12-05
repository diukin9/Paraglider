﻿using System.Security.Claims;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Paraglider.API.DataTransferObjects;
using Paraglider.API.Extensions;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Domain.RDB.Enums;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.Infrastructure.Extensions;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.API.Features.Users.Queries;

public record GetUserByExternalLoginInfoRequest : IRequest<OperationResult<UserDTO>>;

public class GetUserByExternalLoginInfoQueryHandler : IRequestHandler<GetUserByExternalLoginInfoRequest, OperationResult<UserDTO>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IMapper _mapper;

    public GetUserByExternalLoginInfoQueryHandler(
        UserManager<ApplicationUser> userManager,
        IMapper mapper,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _mapper = mapper;
        _signInManager = signInManager;
    }

    public async Task<OperationResult<UserDTO>> Handle(
        GetUserByExternalLoginInfoRequest request, 
        CancellationToken cancellationToken)
    {
        var operation = new OperationResult<UserDTO>();

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
            return operation.AddError(ExceptionMessages.NullOrEmpty(nameof(externalId)));
        }

        //получаем пользователя
        var entity = await _userManager.FindByExternalLoginInfoAsync(provider, externalId);
        if (entity is null)
        {
            return operation.AddError(ExceptionMessages.ObjectNotFound(nameof(ApplicationUser)));
        }

        return operation.AddSuccess(string.Empty, _mapper.Map<UserDTO>(entity));
    }
}
