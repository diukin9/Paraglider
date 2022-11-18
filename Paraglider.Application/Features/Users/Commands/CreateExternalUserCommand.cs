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
using Paraglider.Infrastructure;
using Paraglider.Infrastructure.Exceptions;
using Paraglider.Infrastructure.Extensions;
using Paraglider.Infrastructure.Helpers;
using System.Security.Claims;
using static Paraglider.Infrastructure.AppData;

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
            RuleFor(x => x.Info.LoginProvider).IsEnumName(typeof(ExternalAuthProvider));
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
                return operation.AddError(string.Join("; ", validateResult.Errors), new ArgumentException());
            }

            var provider = Enum.Parse<ExternalAuthProvider>(request.Info.LoginProvider);
            var externalId = request.Info.Principal.Claims
                .SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var firstName = request.Info.Principal.Claims
                .SingleOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value;
            var surname = request.Info.Principal.Claims
                .SingleOrDefault(c => c.Type == ClaimTypes.Surname)?.Value;

            if (new[] { externalId, firstName, surname }.Any(x => string.IsNullOrEmpty(x)))
            {
                return operation.AddError(
                    Exceptions.NotEnoughUserInfoFromExternalProvider,
                    new ExternalAuthException());
            }

            var username = StringHelper.GetExternalUsername(request.Info.LoginProvider, externalId);
            var email = request.Info.Principal.Claims
                .SingleOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

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
                    city = await _cityRepository.GetByKeyAsync(ipInfo.City);
                }
                catch (Exception) { }
            }

            var user = UserFactory.Create(
                new UserFactory.UserData(
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
                return operation.AddError(
                    string.Join("; ", identityResult.Errors),
                    new CreateUserException());
            }

            var result = _mapper.Map<UserDTO>(user);
            return operation.AddSuccess(Messages.ObjectCreated(typeof(ApplicationUser)), result);
        }
    }
}
