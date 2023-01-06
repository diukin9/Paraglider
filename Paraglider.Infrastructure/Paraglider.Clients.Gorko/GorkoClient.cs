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

    public async Task<PagedResult<City>?> GetCitiesAsync(int perPage = 10, int page = 1)
    {
        var request = BuildRequestFor().Cities.WithPaging(new PagingParameters(page, perPage));
        var response = await request.GetResultAsync();
        return response.IsSuccessful ? response.Value : default;
    }

    public async Task<PagedResult<User>?> GetUsersAsync(
        UserRole userRole,
        int perPage = 10,
        int page = 1,
        long? cityId = null)
    {
        var request = BuildRequestFor().Users.WithRole(userRole);
        return await FillWithReviewsAsync(request, perPage, page, cityId);
    }

    public async Task<PagedResult<Restaurant>?> GetRestaurantsAsync(
        int perPage = 10,
        int page = 1,
        long? cityId = null)
    {
        var request = BuildRequestFor().Restaurants;
        return await FillWithReviewsAsync(request, perPage, page, cityId);
    }

    public async Task<PagedResult<Car>?> GetCarsAsync(CarType carType,
        int perPage = 10,
        int page = 1,
        long? cityId = null)
    {
        var request = BuildRequestFor()
            .Cars
            .WithType(carType)
            .FilterBy(car => car.CityId, cityId)
            .WithPaging(new PagingParameters(page, perPage));
        var response = await request.GetResultAsync();
        return response.IsSuccessful ? response.Value : default;
    }

    private async Task<PagedResult<T>?> FillWithReviewsAsync<T>(
        IGorkoResource<T> request,
        int perPage,
        int page,
        long? cityId) where T : IHaveId, IHaveReviews, IHaveCityId
    {
        if (cityId != null)
        {
            request = request.FilterBy(e => e.CityId, cityId);
        }

        request = request.WithPaging(new PagingParameters(page, perPage));
        var response = await request.GetResultAsync();

        if (!response.IsSuccessful) return default;

        foreach (var item in response.Value!.Items)
        {
            item.Reviews = await GetReviewsAsync(item.Id);
        }

        return response.Value;
    }

    private async Task<ICollection<Review>?> GetReviewsAsync(long? id)
    {
        if (id == null) return new HashSet<Review>();

        var request = Endpoints.Reviews(id.Value);
        var response = await httpClient.GetAsync(request);

        try
        {
            var json = await response.Content.ReadAsStringAsync();
            var jObject = JObject.Parse(json);
            var entity = jObject["entity"]!.Children().Single();

            var reviews = entity.Values(key: "review")
                .Select(x => x.ToObject<ICollection<Review>>())
                .Single();

            return reviews;
        }
        catch
        {
            return new HashSet<Review>();
        }
    }
}
