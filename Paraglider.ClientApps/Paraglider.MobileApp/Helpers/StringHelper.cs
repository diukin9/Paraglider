using System.Net.Mail;

namespace Paraglider.MobileApp.Helpers;

public static class StringHelper
{
    public static bool IsEmail(string value)
    {
        if (string.IsNullOrEmpty(value)) return false;
        return MailAddress.TryCreate(value.Trim().ToLower(), out var result);
    }
}
