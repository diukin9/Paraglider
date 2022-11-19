using MediatR;
using Microsoft.AspNetCore.Identity;
using Paraglider.Domain.Entities;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Extensions;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.API.Features.Users.Queries
{
    public class GetExternalLoginInfoRequest : IRequest<OperationResult>
    {

    }

    public class GetExternalLoginInfoQueryHandler : IRequestHandler<GetExternalLoginInfoRequest, OperationResult>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public GetExternalLoginInfoQueryHandler(
            SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<OperationResult> Handle(
            GetExternalLoginInfoRequest request,
            CancellationToken cancellationToken)
        {
            var operation = new OperationResult();

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info is null)
            {
                return operation.AddError(ExceptionMessages.ObjectIsNull(typeof(ExternalLoginInfo)));
            }

            return operation.AddSuccess(Messages.ObjectFound(info!.GetType()), info);
        }
    }
}
