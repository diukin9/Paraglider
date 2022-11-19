namespace Paraglider.GorkoClient.Extensions;

public static class UriExtensions
{
    /// <summary>
    /// Добавляет query-параметр к URI
    /// </summary>
    /// <param name="uri">Исходный uri</param>
    /// <param name="key">Ключ параметра</param>
    /// <param name="value">Значение параметра</param>
    /// <returns>Новый URI с query-параметром</returns>
    public static Uri AddQueryParameter(this Uri uri, string key, string value)
    {
        var resultQuery = uri.AbsoluteUri + (string.IsNullOrEmpty(uri.Query)
            ? $"?{key}={value}"
            : $"&{key}={value}");
        return new Uri(resultQuery);
    }
}