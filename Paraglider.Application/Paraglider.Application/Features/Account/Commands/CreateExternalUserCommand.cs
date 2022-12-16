using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Paraglider.Application.DataTransferObjects;
using Paraglider.Data.EntityFrameworkCore.Factories;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Domain.RDB.Enums;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.Infrastructure.Common.Helpers;
using Paraglider.Infrastructure.Common.Response;
using System.Security.Claims;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.Application.Features.Account.Commands;

public record CreateExternalUserRequest() : IRequest<OperationResult<UserDTO>>;

public class CreateExternalUserCommandHandler
    : IRequestHandler<CreateExternalUserRequest, OperationResult<UserDTO>>
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessor _accessor;
    private readonly ICityRepository _cityRepository;
    private readonly IMapper _mapper;

    public CreateExternalUserCommandHandler(
        ICityRepository cityRepository,
        UserManager<ApplicationUser> userManager,
        IHttpContextAccessor accessor,
        SignInManager<ApplicationUser> signInManager,
        IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _cityRepository = cityRepository;
        _accessor = accessor;
        _mapper = mapper;
    }

    public async Task<OperationResult<UserDTO>> Handle(
        CreateExternalUserRequest request,
        CancellationToken cancellationToken)
    {
        var operation = new OperationResult<UserDTO>();

        //получаем ExternalLoginInfo
        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info is null)
        {
            return operation.AddError(ExceptionMessages.ObjectNotFound(nameof(ExternalLoginInfo)));
        }

        //получаем от провайдера необходимые данные для создания пользователя
        var provider = Enum.Parse<AuthProvider>(info.LoginProvider);
        var externalId = info.Principal.Claims.GetByClaimType(ClaimTypes.NameIdentifier);
        var firstName = info.Principal.Claims.GetByClaimType(ClaimTypes.GivenName);
        var surname = info.Principal.Claims.GetByClaimType(ClaimTypes.Surname);

        if (new[] { externalId, firstName, surname }.Any(string.IsNullOrEmpty))
        {
            var message = "Не удалось получить основную информацию о пользователе от внешнего провайдера.";
            return operation.AddError(message);
        }

        //формируем username внешнего пользователя и пытаемся получить почту, если провайдер делится ею
        var username = StringHelper.GetExternalUsername(info.LoginProvider, externalId);
        var email = info.Principal.Claims.GetByClaimType(ClaimTypes.Email);

        //пытаемся получить город пользователя на основе его ip
        var cityName =  await InternetProtocolHelper.GetInfoAsync(_accessor.HttpContext);
        var city = await _cityRepository.FindByNameAsync(cityName?.City ?? string.Empty);

        //создаем пользователя через фабрику
        var user = UserFactory.Create(
            new UserData(
                firstName: firstName!,
                surname: surname!,
                username: username,
                city: city ?? await _cityRepository.GetDefaultCity(),
                email: email,
                emailConfirmed: email is not null,
                provider: provider,
                externalId: externalId));

        //добавляем пользователя в базу данных
        var identityResult = await _userManager.CreateAsync(user);
        if (!identityResult.Succeeded)
        {
            return operation.AddError(string.Join("; ", identityResult.Errors));
        }

        return operation.AddSuccess(string.Empty, _mapper.Map<UserDTO>(user));
    }
}
