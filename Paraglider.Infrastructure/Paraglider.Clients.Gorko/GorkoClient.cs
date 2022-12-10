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

        return FillWithReviews(ComponentType.User, usersResource, perPage, page, cityId);
    }

    public IAsyncEnumerable<Restaurant> GetRestaurantsAsync(int perPage = 10,
        int page = 1,
        long? cityId = null)
    {
        var restaurantResource = BuildRequestFor()
            .Restaurants;

        return FillWithReviews(ComponentType.Restaurant, restaurantResource, perPage, page, cityId);
    }

    private async IAsyncEnumerable<T> FillWithReviews<T>(
        ComponentType type,
        IGorkoResource<T> resource,
        int perPage,
        int page,
        long? cityId) where T : IHaveId, IHaveReviews, IHaveCityId
    {
        if (cityId != null)
            resource = resource.FilterBy(e => e.CityId, cityId);

        var response = await resource
            .WithPaging(new PagingParameters(page, perPage))
            .GetResult();

        if (!response.IsSuccessful)
            yield break;

        foreach (var item in response.Value!.Items)
        {
            item.Reviews = await GetReviews(type, item.Id);
            yield return item;
        }
    }

    private async Task<List<Review>> GetReviews(ComponentType type, long? id)
    {
        if (id == null)
            return new List<Review>();
        
        var request = type switch
        {
            ComponentType.User => Endpoints.UserReviews(id.Value),
            ComponentType.Restaurant => Endpoints.RestaurantReviews(id.Value),
            _ => throw new NotImplementedException()
        };

        var response = await httpClient.GetAsync(request);

        try
        {
            var json = await response.Content.ReadAsStringAsync();
            var jsonObject = JObject.Parse(json);

            var reviews = jsonObject["reviews"]
                ?.Select(x => x.ToObject<Review>())
                .ToList() ?? new List<Review?>();

            return reviews!;
        }
        catch
        {
            return null!;
        }
    }
}
