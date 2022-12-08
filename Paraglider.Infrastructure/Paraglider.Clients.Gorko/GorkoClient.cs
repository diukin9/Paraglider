using Newtonsoft.Json.Linq;
using Paraglider.Clients.Gorko.Configurations;
using Paraglider.Clients.Gorko.Models;
using Paraglider.Clients.Gorko.Models.Abstractions;
using Paraglider.Clients.Gorko.Models.Enums;
using Paraglider.Clients.Gorko.Resources;

namespace Paraglider.Clients.Gorko;

public class GorkoClient
{
    private readonly HttpClient httpClient;

    public GorkoClient(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public IGorkoClientConfiguration BuildRequestFor()
    {
        return new GorkoClientConfiguration(httpClient);
    }

    public async IAsyncEnumerable<City> GetCitiesAsync(int perPage = 10, int page = 1)
    {
        var cities = await BuildRequestFor().Cities
            .WithPaging(new PagingParameters(page, perPage))
            .GetResult();

        if (!cities.IsSuccessful) yield break;

        foreach (var city in cities.Value!.Items)
        {
            yield return city;
        }
    }

    public IAsyncEnumerable<User> GetUsersAsync(UserRole userRole,
        int perPage = 10,
        int page = 1,
        long? cityId = null)
    {
        var usersResource = BuildRequestFor()
            .Users
            .WithRole(userRole);

        return FillWithReviews(usersResource, perPage, page, cityId);
    }

    public IAsyncEnumerable<Restaurant> GetRestaurantsAsync(int perPage = 10,
        int page = 1,
        long? cityId = null)
    {
        var restaurantResource = BuildRequestFor()
            .Restaurants;

        return FillWithReviews(restaurantResource, perPage, page, cityId);
    }

    private async IAsyncEnumerable<T> FillWithReviews<T>(IGorkoResource<T> resource,
        int perPage,
        int page,
        long? cityId) where T : IHaveId, IHaveReviews, IHaveCityId
    {
        if (cityId != null)
            resource = resource.FilterBy(e => e.CityId, cityId);

        var restaurants = await resource
            .WithPaging(new PagingParameters(page, perPage))
            .GetResult();

        if (!restaurants.IsSuccessful)
            yield break;

        foreach (var restaurant in restaurants.Value!.Items)
        {
            restaurant.Reviews = await GetReviews(restaurant.Id);
            yield return restaurant;
        }
    }

    private async Task<List<Review>> GetReviews(long? id)
    {
        if (id == null)
            return new List<Review>();
        
        var response = await httpClient.GetAsync(Endpoints.RestaurantReviews(id.Value));

        var json = await response.Content.ReadAsStringAsync();
        var jsonObject = JObject.Parse(json);

        var reviews = jsonObject["reviews"]
            ?.Select(x => x.ToObject<Review>())
            .ToList() ?? new List<Review?>();

        return reviews!;
    }
}
