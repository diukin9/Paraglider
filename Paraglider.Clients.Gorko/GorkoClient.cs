using Newtonsoft.Json.Linq;
using Paraglider.GorkoClient.Configurations;
using Paraglider.GorkoClient.Models;
using Paraglider.GorkoClient.Models.Abstractions;
using Paraglider.GorkoClient.Models.Enums;
using Paraglider.GorkoClient.Resources;

namespace Paraglider.GorkoClient;

public static class GorkoClient
{
    public static IGorkoClientConfiguration BuildRequestFor()
    {
        return new GorkoClientConfiguration();
    }

    public static IAsyncEnumerable<User> GetUsersAsync(UserRole userRole,
        int perPage = 10,
        int page = 1,
        int? cityId = null)
    {
        var usersResource = BuildRequestFor()
            .Users
            .WithRole(userRole);

        return FillWithReviews(usersResource, perPage, page, cityId);
    }

    public static IAsyncEnumerable<Restaurant> GetRestaurantsAsync(int perPage = 10,
        int page = 1,
        int? cityId = null)
    {
        var restaurantResource = BuildRequestFor()
            .Restaurants;

        return FillWithReviews(restaurantResource, perPage, page, cityId);
    }

    private static async IAsyncEnumerable<T> FillWithReviews<T>(IGorkoResource<T> resource,
        int perPage,
        int page,
        int? cityId) where T : IHaveId, IHaveReviews, IHaveCityId
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

    private static async Task<List<Review>> GetReviews(long? id)
    {
        if (id == null)
            return new List<Review>();
        
        using var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(Endpoints.RestaurantReviews(id.Value));

        var json = await response.Content.ReadAsStringAsync();
        var jsonObject = JObject.Parse(json);

        var reviews = jsonObject["reviews"]
            ?.Select(x => x.ToObject<Review>())
            .ToList() ?? new List<Review?>();

        return reviews!;
    }
}
