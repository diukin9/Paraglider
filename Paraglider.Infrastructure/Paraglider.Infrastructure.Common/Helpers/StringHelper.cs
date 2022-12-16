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

    public static bool CompareLetters(string? first, string? second)
    {
        if (first is null || second is null) return false;
        var format = (string str) => GetLetters(str).ToUpper().Normalize();
        return format(first) == format(second);
    }

    private static string GetLetters(string str)
    {
        return new string(str.Where(c => char.IsLetter(c)).ToArray());
    }
}
