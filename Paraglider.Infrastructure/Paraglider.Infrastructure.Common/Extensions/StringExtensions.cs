using System.Text.RegularExpressions;

namespace Paraglider.Infrastructure.Common.Extensions;

public static partial class StringExtensions
{
    public static string? ToPhoneNumberOrDefault(this string value)
    {
        if (string.IsNullOrEmpty(value)) throw new ArgumentException();

        value = string.Concat(value.Where(char.IsDigit));

        var regex = new[] { WithInternationalCode(), WithOutInternationalCode() };

        if (!regex.Any(x => x.Match(value).Success)) return default;

        if (value.Length == 11) value = string.Concat(value.Skip(1));

        var cityCode = value[..3];

        value = string.Concat(value.Skip(3));

        var numberRange = $"{value[..3]}-{value[3..5]}-{value[^2..]}";

        return $"+7({cityCode}){numberRange}";
    }

    [GeneratedRegex("[7|8][0-9]{10}")]
    private static partial Regex WithInternationalCode();

    [GeneratedRegex("[9][0-9]{9}")]
    private static partial Regex WithOutInternationalCode();
}
