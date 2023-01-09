using Paraglider.MobileApp.Models;
using System.Diagnostics;
using System.Text.Json;
using static Paraglider.MobileApp.Constants;

namespace Paraglider.MobileApp.Services;

public class CityService
{
    private readonly HttpClient httpClient;

    public CityService(HttpClient httpClient)
    {;
        this.httpClient = httpClient;
    }

    public async Task<IEnumerable<City>> GetCitiesAsync()
    {
        try
        {
            var url = $"{API_URL}/cities";
            var response = await httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<ResponseTemplate<IEnumerable<City>>>(content);

            var check = await response.Content.ReadAsStringAsync();
            var check1 = JsonSerializer.Deserialize<ResponseTemplate<IEnumerable<City>>>(check);
            var check2 = JsonSerializer.Deserialize<ResponseTemplate<List<City>>>(check);
            var check3 = JsonSerializer.Deserialize<ResponseTemplate<City[]>>(check);


            return result.Metadata.DataObject;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Failed to send password recovery email: {ex.Message}");
            return null;
        }
    }
}
