using System.Text.RegularExpressions;

namespace Paraglider.Infrastructure.Extensions;

public static class StringExtensions
{
    public static bool IsEmail(this string value)
    {
        var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        return regex.Match(value).Success;
    }

    public static string ToPhoneNumberPattern(this string value)
    {
        throw new NotImplementedException();
    }
}
