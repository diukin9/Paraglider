using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Paraglider.API.DataTransferObjects;
using Paraglider.Data.EntityFrameworkCore.Factories;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Domain.RDB.Enums;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.Infrastructure.Common.Helpers;
using Paraglider.Infrastructure.Common.Response;
using Paraglider.Infrastructure.Extensions;
using System.Security.Claims;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.API.Features.Users.Commands;

public record CreateExternalUserRequest() : IRequest<OperationResult>;

public class CreateExternalUserCommandHandler : IRequestHandler<CreateExternalUserRequest, OperationResult>
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

    public async Task<OperationResult> Handle(
        CreateExternalUserRequest request,
        CancellationToken cancellationToken)
    {
        var operation = new OperationResult();

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
            return operation.AddError(ExceptionMessages.NotEnoughUserInfoFromExternalProvider);
        }

        //формируем username внешнего пользователя и пытаемся получить почту, если провайдер делится ею
        var username = StringHelper.GetExternalUsername(info.LoginProvider, externalId);
        var email = info.Principal.Claims.GetByClaimType(ClaimTypes.Email);

        //пытаемся получить город пользователя на основе его ip
        var city = await this.GetCityByUserIpAsync();

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

        var result = _mapper.Map<UserDTO>(user);
        return operation.AddSuccess(Messages.ObjectCreated(nameof(ApplicationUser)), result);
    }

    private async Task<City?> GetCityByUserIpAsync()
    {
        var city = default(City);

        //получаем ip пользователя
        var ip = _accessor.HttpContext?.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress?.ToString();
        if (ip is not null)
        {
            try
            {
                //получаем данные о местоположении
                var response = await new HttpClient().GetAsync($"http://ipinfo.io/{ip}");
                if (!response.IsSuccessStatusCode || response.Content is null) throw new Exception();
                var content = await response!.Content!.ReadAsStringAsync();
                var ipInfo = JsonConvert.DeserializeObject<IpInfo>(content);

                //получаем application city
                if (ipInfo?.City is not null) city = await _cityRepository.GetByNameAsync(ipInfo.City);
            }
            catch (Exception) { }
        }
        return city;
    }
}
