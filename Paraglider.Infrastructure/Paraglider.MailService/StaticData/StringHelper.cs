namespace Paraglider.MailService;

public static class StringHelper
{
    public static bool CheckForNull(params string[] values)
    {
        foreach (var value in values)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                return true;
            }
        }

        return false;
    }
}
