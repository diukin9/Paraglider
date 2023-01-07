using System.Net.Http.Headers;
using System.Text.Json;

namespace Paraglider.MobileApp.Services;

public class RestService
{
    private readonly HttpClient httpClient;
    private readonly StorageService storageService;
    private readonly JsonSerializerOptions serializerOptions;

    public RestService(HttpClient httpClient, StorageService storageService)
    {
        this.httpClient = httpClient;
        this.storageService = storageService;
        serializerOptions = new JsonSerializerOptions();
    }

    public async Task<T> PostAsync<T>(string url,HttpContent content = null, bool needAuth = false)
    {
        return await SendAsync<T>(url, HttpMethod.Post, content, needAuth);
    }

    public async Task<T> PutAsync<T>(string url, HttpContent content = null, bool needAuth = false)
    {
        return await SendAsync<T>(url, HttpMethod.Put, content, needAuth);
    }

    private async Task<T> SendAsync<T>(
        string url,
        HttpMethod method,
        HttpContent httpContent = null,
        bool needAuth = false)
    {
        if (string.IsNullOrEmpty(url) || method is null)
        {
            throw new ArgumentException("Wrong parameters");
        }

        var request = new HttpRequestMessage()
        {
            RequestUri = new Uri(url),
            Method = HttpMethod.Post
        };

        if (method != HttpMethod.Get && httpContent is not null)
        {
            request.Content = httpContent;
        }

        if (needAuth)
        {
            var access_token = await storageService.GetTokenAsync();
            if (access_token is null) throw new UnauthorizedAccessException();

            var header = new AuthenticationHeaderValue("Bearer", access_token);
            request.Headers.Authorization = header;
        }

        var response = await httpClient.SendAsync(request);
        var content = await response.Content.ReadAsStreamAsync();

        return await JsonSerializer.DeserializeAsync<T>(
            options: serializerOptions,
            utf8Json: content);
    }
}
