using System.Linq.Expressions;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Paraglider.Clients.Gorko.Extensions;
using Paraglider.Clients.Gorko.Models;
using Paraglider.Clients.Gorko.Models.Enums;
using Paraglider.Clients.Gorko.Resources;
using Paraglider.Clients.Gorko.Result;

namespace Paraglider.Clients.Gorko.Configurations;

internal class GorkoClientConfiguration<T> :
    GorkoClientConfiguration,
    IGorkoResource<T>,
    IUsersResource,
    ICarsResource
{
    private readonly Uri uri;

    internal GorkoClientConfiguration(Uri uri)
    {
        this.uri = uri;
    }

    public IGorkoResource<Car> WithType(CarType carType)
    {
        var typeId = ((int) carType).ToString();
        return new GorkoClientConfiguration<Car>(uri.AddQueryParameter("type_id", typeId));
    }

    /// <summary>
    ///     Фильтрует результат по заданному свойству
    /// </summary>
    /// <param name="selector">Селектор свойства</param>
    /// <param name="value">Значение, которому должно быть равно свойство</param>
    /// <typeparam name="TProp">Тип свойства</typeparam>
    /// <returns></returns>
    /// <remarks>
    ///     Свойство не должно быть сложным типом. Если указан атрибут JsonProperty с именем свойства, то использует его
    ///     для фильрации, иначе - преобразует в snake_case имя свойства
    /// </remarks>
    public IGorkoResource<T> FilterBy<TProp>(Expression<Func<T, TProp>> selector,
        TProp value)
    {
        var property = ((MemberExpression) selector.Body).Member;
        var attributeInfo = property.GetCustomAttribute<JsonPropertyAttribute>();
        var propertyName = attributeInfo?.PropertyName;
        var parameterName = propertyName ?? new SnakeCaseNamingStrategy().GetPropertyName(property.Name, false);
        return new GorkoClientConfiguration<T>(uri.AddQueryParameter(parameterName, value.ToString()));
    }

    public IGorkoResource<T> WithPaging(PagingParameters pagingParameters)
    {
        return new GorkoClientConfiguration<T>(uri
            .AddQueryParameter("per_page", pagingParameters.PerPage.ToString())
            .AddQueryParameter("page", pagingParameters.PageNumber.ToString()));
    }

    public async Task<Result<PagedResult<T>?>> GetResult()
    {
        using var httpClient = new HttpClient();
        var result = await httpClient.GetAsync<PagedResult<T>>(uri);

        return result;
    }

    public IGorkoResource<User> WithRole(UserRole userRole)
    {
        var roleId = ((int) userRole).ToString();
        return new GorkoClientConfiguration<User>(uri.AddQueryParameter("role_id", roleId));
    }

    private static async Task<List<Review>> GetReviews(Uri uri)
    {
        using var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(uri);

        var json = await response.Content.ReadAsStringAsync();
        var jsonObject = JObject.Parse(json);

        var reviews = jsonObject["reviews"]
            ?.Select(x => x.ToObject<Review>())
            .ToList() ?? new List<Review?>();

        return reviews!;
    }
}

public class GorkoClientConfiguration : IGorkoClientConfiguration
{
    public IUsersResource Users => CreateConfiguration<User>(Endpoints.Users);

    public IGorkoResource<Role> Roles => CreateConfiguration<Role>(Endpoints.Roles);

    public IGorkoResource<City> Cities => CreateConfiguration<City>(Endpoints.Cities);
    public IGorkoResource<Restaurant> Restaurants => CreateConfiguration<Restaurant>(Endpoints.Restaurants);

    public ICarsResource Cars => CreateConfiguration<Car>(Endpoints.Cars);

    private GorkoClientConfiguration<T> CreateConfiguration<T>(string resourceName)
    {
        return new(new Uri(Endpoints.BaseUrl, resourceName));
    }
}