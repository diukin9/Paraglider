using System.Text.Json.Serialization;

namespace Paraglider.Infrastructure.Common.Response;

public class ErrorResponse
{
    [JsonPropertyName("message")]
    public string? Message { get; set; }

    public static ErrorResponse? Create<T>(InternalOperation<T> internalResponse)
    {
        if (internalResponse?.Metadata?.Message is not null)
        {
            return new ErrorResponse() { Message = internalResponse?.Metadata?.Message };
        }

        return null;
    }
}
