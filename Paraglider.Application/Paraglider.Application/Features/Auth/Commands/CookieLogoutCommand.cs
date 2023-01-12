using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Paraglider.Infrastructure.Common.Response;

namespace Paraglider.Application.Features.Auth.Commands;

public class CookieLogoutRequest : IRequest<InternalOperation>
{

}

public class CookieLogoutCommandHandler : IRequestHandler<CookieLogoutRequest, InternalOperation>
{
    private readonly IHttpContextAccessor _accessor;

    public CookieLogoutCommandHandler(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    public async Task<InternalOperation> Handle(
        CookieLogoutRequest request,
        CancellationToken cancellationToken)
    {
        var operation = new InternalOperation();
        var scheme = CookieAuthenticationDefaults.AuthenticationScheme;
        await _accessor.HttpContext!.SignOutAsync(scheme);
        return operation.AddSuccess();
    }
}
