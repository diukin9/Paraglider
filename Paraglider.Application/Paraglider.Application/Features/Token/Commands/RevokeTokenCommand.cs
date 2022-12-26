using MediatR;
using Microsoft.Net.Http.Headers;
using Paraglider.Infrastructure.Common.Response;

namespace Paraglider.Application.Features.Token.Commands;

public record RevokeTokenRequest() : IRequest<OperationResult>;

public class RevokeTokenCommandHandler : IRequestHandler<RevokeTokenRequest, OperationResult>
{
    private readonly IHttpContextAccessor _accessor;

    public RevokeTokenCommandHandler(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    public async Task<OperationResult> Handle(
        RevokeTokenRequest request, 
        CancellationToken cancellationToken)
    {
        var operation = new OperationResult();
        try
        {
            _accessor.HttpContext!.Response.Headers.Remove(HeaderNames.Authorization);
            return await Task.FromResult(
                operation.AddSuccess("Пользователь успешно вышел из аккаунта."));
        }
        catch
        {
            return await Task.FromResult(
                operation.AddSuccess("Не удалось выйти из аккаунта."));
        }
    }
}
