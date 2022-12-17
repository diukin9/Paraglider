namespace Paraglider.Infrastructure.Common;

public static partial class AppData
{
    public static class Messages
    {
        public static string ObjectUpdated(string name) => $"The '{name}' successfully updated.";

        public static string ObjectUpdated(string name, params string[] propertyNames)
        {
            return $"The '{name}' successfully updated: " + string.Join(", ", propertyNames);
        }
    }
}