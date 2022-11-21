using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Paraglider.Clients.Gorko.Result;

namespace Paraglider.Clients.Gorko.Extensions;

internal static class HttpClientExtensions
{
    internal static async Task<Result<TResponse?>> GetAsync<TResponse>(this HttpClient httpClientImplementation,
        Uri uri)
    {
        try
        {
            var response = await httpClientImplementation.GetAsync(uri);

            if (!response.IsSuccessStatusCode)
                return Result<TResponse?>.Error(response.StatusCode);

            var deserializedResult =
                JsonConvert.DeserializeObject<TResponse>(await response.Content.ReadAsStringAsync(),
                    new JsonSerializerSettings
                    {
                        ContractResolver = new DefaultContractResolver {NamingStrategy = new SnakeCaseNamingStrategy()}
                    });
            return Result<TResponse?>.Ok(deserializedResult);
        }
        catch(Exception e)
        {
            return Result<TResponse?>.Error(null, exception: e);
        }

    }
}