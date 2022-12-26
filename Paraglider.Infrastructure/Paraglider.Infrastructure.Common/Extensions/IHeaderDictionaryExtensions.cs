using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace Paraglider.Infrastructure.Common.Extensions;

public static class IHeaderDictionaryExtensions
{
    public static string? GetNameIdentifierFromBearerToken(this IHeaderDictionary headers)
    {
        return headers.ContainsKey(HeaderNames.Authorization)
            ? headers[HeaderNames.Authorization].ToString().Replace("Bearer ", string.Empty)
            : null;
    }
}
