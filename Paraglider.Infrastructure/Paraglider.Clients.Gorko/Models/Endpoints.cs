namespace Paraglider.Clients.Gorko.Models;

internal static class Endpoints
{
    internal static Uri BaseUrl => new("https://api.gorko.ru/api/");

    internal static string Users => "v2/users?fields=catalog_media,specs,text";

    internal static Uri Reviews(long id)
    {
        return new(BaseUrl, $"v3/venue/{id}?entity[languageId]=1");
    }

    internal static string Cars => "v2/cars?fields=media,params,contacts,text";

    internal static string Roles => "v2/roles";

    internal static string Cities => "v2/cities";

    internal static string Restaurants => "v2/restaurants?fields=text";
}

public class PagingParameters
{
    /// <summary>
    /// Конструктор PagingParameters
    /// </summary>
    /// <param name="pageNumber">Номер страницы</param>
    /// <param name="perPage">Кол-во элементов на странице</param>
    /// <exception cref="ArgumentException">Все аргументы должны быть больше нуля</exception>
    public PagingParameters(int pageNumber, int perPage)
    {
        if (pageNumber <= 0)
            throw new ArgumentException("Номер страницы должен быть больше нуля", nameof(pageNumber));
        if (perPage <= 0)
            throw new ArgumentException("Кол-во элементов на странице должно быть больше нуля");
        
        PageNumber = pageNumber;
        PerPage = perPage;
    }

    public int PageNumber { get; }
    public int PerPage { get; }
}