using MediatR;
using Microsoft.AspNetCore.Identity;
using Paraglider.Domain.Entities;
using Paraglider.Infrastructure;
using Paraglider.Infrastructure.Exceptions;
using Paraglider.Infrastructure.Extensions;
using static Paraglider.Infrastructure.AppData;

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
                operation.AddError(
                    Exceptions.ObjectIsNull(typeof(ExternalLoginInfo)),
                    new NotFoundException(typeof(ExternalLoginInfo)));
            }

            operation.AddSuccess(Messages.ObjectFound(info!.GetType()), info);
            return operation;
        }
    }
}
