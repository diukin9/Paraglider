using Microsoft.AspNetCore.Mvc;
using Paraglider.Infrastructure.Common.Response;
using System.Net;

namespace Paraglider.Infrastructure.Common.Helpers;

public static class ActionResult
{
    public static IActionResult Create<T>(InternalOperation<T> operation, HttpStatusCode? statusCode = default)
    {
        ObjectResult response;

        if (operation?.Metadata is null) throw new ArgumentNullException(nameof(operation));

        statusCode ??= operation.IsOk ? HttpStatusCode.OK : HttpStatusCode.BadRequest;

        if (200 <= (int)statusCode.Value && (int)statusCode.Value < 300)
        {
            response = new ObjectResult(operation.Metadata.DataObject);
        }
        else
        {
            response = new ObjectResult(ErrorResponse.Create(operation));
        }

        response.StatusCode = (int)statusCode.Value;

        return response;
    }
}

