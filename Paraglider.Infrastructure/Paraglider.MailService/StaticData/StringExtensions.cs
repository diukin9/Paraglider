using System.Text.RegularExpressions;

namespace Paraglider.MailService;

public static partial class StringExtensions
{
    public static bool IsEmail(this string value)
    {
        var regex = EmailRegex();
        return regex.Match(value).Success;
    }

    [GeneratedRegex("^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$")]
    private static partial Regex EmailRegex();
}
