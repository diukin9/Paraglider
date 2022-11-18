using FluentValidation;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Paraglider.API.DataTransferObjects;
using Paraglider.Domain.Entities;
using Paraglider.Domain.Enums;
using Paraglider.Infrastructure;
using Paraglider.Infrastructure.Exceptions;
using Paraglider.Infrastructure.Extensions;
using System.Security.Claims;
using static Paraglider.Infrastructure.AppData;

namespace Paraglider.API.Features.Users.Queries
{
    public class GetUserByExternalLoginInfoRequest : IRequest<OperationResult>
    {
        public ExternalLoginInfo Info { get; set; }

        public GetUserByExternalLoginInfoRequest(ExternalLoginInfo info) 
        {
            Info = info;
        }
    }

    public class GetUserByExternalLoginInfoRequestValidator : AbstractValidator<GetUserByExternalLoginInfoRequest>
    {
        public GetUserByExternalLoginInfoRequestValidator() => RuleSet(AppData.DefaultRuleSetName, () =>
        {
            RuleFor(x => x.Info).NotNull();
            RuleFor(x => x.Info.LoginProvider).IsEnumName(typeof(ExternalAuthProvider));
        });
    }

    public class GetUserByExternalLoginInfoQueryHandler : IRequestHandler<GetUserByExternalLoginInfoRequest, OperationResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IValidator<GetUserByExternalLoginInfoRequest> _validator;
        private readonly IMapper _mapper;

        public GetUserByExternalLoginInfoQueryHandler(
            UserManager<ApplicationUser> userManager, 
            IValidator<GetUserByExternalLoginInfoRequest> validator,
            IMapper mapper)
        {
            _userManager = userManager;
            _validator = validator;
            _mapper = mapper; 
        }

        public async Task<OperationResult> Handle(
            GetUserByExternalLoginInfoRequest request, 
            CancellationToken cancellationToken)
        {
            var operation = new OperationResult();

            var validateResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validateResult.IsValid)
            {
                return operation.AddError(string.Join("; ", validateResult.Errors), new ArgumentException());
            }

            var externalId = request.Info.Principal.Claims
                .Where(c => c.Type == ClaimTypes.NameIdentifier)
                .SingleOrDefault()
                ?.Value;

            if (string.IsNullOrEmpty(externalId))
            {
                return operation.AddError(
                    Exceptions.NotEnoughUserInfoFromExternalProvider,
                    new ExternalAuthException());
            }

            var provider = Enum.Parse<ExternalAuthProvider>(request.Info.LoginProvider);

            var user = await _userManager.Users
                .Include(x => x.ExternalAuthInfo)
                .Include(x => x.City)
                .Where(x => x.ExternalAuthInfo
                    .Any(y => y.ExternalProvider == provider && y.ExternalId == externalId))
                .SingleOrDefaultAsync();

            var dto = _mapper.Map<UserDTO>(user!);

            if (dto is null)
            {
                return operation.AddError(
                    Exceptions.ObjectIsNull(typeof(ApplicationUser)),
                    new NotFoundException(typeof(ApplicationUser)));
            }

            operation.AddSuccess(Messages.ObjectFound(typeof(ApplicationUser)), dto);
            return operation;
        }
    }
}
