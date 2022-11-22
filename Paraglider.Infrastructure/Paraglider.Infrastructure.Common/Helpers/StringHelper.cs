namespace Paraglider.Infrastructure.Common.Helpers;

public static class StringHelper
{
    public static string GetExternalUsername(string provider, string? externalId)
    {
        if (string.IsNullOrEmpty(provider))
        {
            throw new ArgumentNullException(nameof(provider));
        }
        else if (string.IsNullOrEmpty(externalId))
        {
            throw new ArgumentNullException(nameof(externalId));
        }

        return $"{provider}-{externalId}";
    }
}
