using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json;

namespace Paraglider.Infrastructure.Common.Helpers;

public static class InternetProtocolHelper
{
    public static async Task<IpInfo?> GetInfoAsync(HttpContext? context)
    {
        try
        {
            //получаем данные о местоположении
            var ip = context?.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress?.ToString();
            var response = await new HttpClient().GetAsync($"http://ipinfo.io/{ip}");
            if (!response.IsSuccessStatusCode || response.Content is null) throw new Exception();
            var content = await response!.Content!.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IpInfo>(content);
        }
        catch 
        {
            return null;
        }
    }
}
