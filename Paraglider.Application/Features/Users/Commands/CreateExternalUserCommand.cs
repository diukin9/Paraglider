using FluentValidation;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Paraglider.API.DataTransferObjects;
using Paraglider.Data.Factories;
using Paraglider.Data.Repositories;
using Paraglider.Domain.Entities;
using Paraglider.Domain.Enums;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Abstractions;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.Infrastructure.Common.Helpers;
using Paraglider.Infrastructure.Extensions;
using System.Security.Claims;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.API.Features.Users.Commands
{
    public class CreateExternalUserRequest : IRequest<OperationResult>
    {
        public ExternalLoginInfo Info { get; set; } = null!;
    }

    public class CreateExternalUserRequestValidator : AbstractValidator<CreateExternalUserRequest>
    {
        public CreateExternalUserRequestValidator() => RuleSet(DefaultRuleSetName, () =>
        {
            RuleFor(x => x.Info).NotNull();
            RuleFor(x => x.Info.LoginProvider).IsEnumName(typeof(ExternalAuthProvider), false);
        });
    }

    public class CreateExternalUserCommandHandler : IRequestHandler<CreateExternalUserRequest, OperationResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _accessor;
        private readonly CityRepository _cityRepository;
        private readonly IValidator<CreateExternalUserRequest> _validator;
        private readonly IMapper _mapper;

        public CreateExternalUserCommandHandler(
            UserManager<ApplicationUser> userManager,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor accessor,
            IValidator<CreateExternalUserRequest> validator,
            IMapper mapper)
        {
            _userManager = userManager;
            _cityRepository = (CityRepository)unitOfWork.GetRepository<City>();
            _accessor = accessor;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<OperationResult> Handle(
            CreateExternalUserRequest request,
            CancellationToken cancellationToken)
        {
            var operation = new OperationResult();

            var validateResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validateResult.IsValid)
            {
                return operation.AddError(string.Join("; ", validateResult.Errors));
            }

            var provider = Enum.Parse<ExternalAuthProvider>(request.Info.LoginProvider);
            var externalId = request.Info.Principal.Claims.GetByClaimType(ClaimTypes.NameIdentifier);
            var firstName = request.Info.Principal.Claims.GetByClaimType(ClaimTypes.GivenName);
            var surname = request.Info.Principal.Claims.GetByClaimType(ClaimTypes.Surname);

            if (new[] { externalId, firstName, surname }.Any(string.IsNullOrEmpty))
            {
                return operation.AddError(ExceptionMessages.NotEnoughUserInfoFromExternalProvider);
            }

            var username = StringHelper.GetExternalUsername(request.Info.LoginProvider, externalId);
            var email = request.Info.Principal.Claims.GetByClaimType(ClaimTypes.Email);

            var city = default(City);
            //получаем ip пользователя (на локальном сервере всегда будет ::1)
            var ip = _accessor.HttpContext?.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress?.ToString();
            if (ip is not null)
            {
                var ipInfo = new IpInfo();
                try
                {
                    //получаем данные о местоположении
                    var response = await new HttpClient().GetAsync($"http://ipinfo.io/{ip}");
                    if (!response.IsSuccessStatusCode || response.Content is null) throw new Exception();
                    var content = await response!.Content!.ReadAsStringAsync();
                    ipInfo = JsonConvert.DeserializeObject<IpInfo>(content);
                    //получаем application city
                    if (ipInfo?.City is not null) city = await _cityRepository.GetByKeyAsync(ipInfo.City);
                }
                catch (Exception) { }
            }

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

            var identityResult = await _userManager.CreateAsync(user);
            if (!identityResult.Succeeded)
            {
                return operation.AddError(string.Join("; ", identityResult.Errors));
            }

            var result = _mapper.Map<UserDTO>(user);
            return operation.AddSuccess(Messages.ObjectCreated(typeof(ApplicationUser)), result);
        }
    }
}
