namespace Paraglider.MailService;

public static class StringHelper
{
    public static bool CheckForNullOrEmpty(params string[] values)
    {
        return values.Any(string.IsNullOrEmpty);
    }
}
