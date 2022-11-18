namespace Paraglider.Infrastructure;

public static partial class AppData
{
    public static class Messages
    {
        public const string SuccessfullExternalAuth = "The user is successfully authorized using the external provider.";
        public const string SuccessfulAuth = "The user is successfully authorized.";
        public const string SuccessfulLogout = "User logged out.";
        public static string ObjectFound(Type type) => $"The '{type.Name}' object successfully found.";
        public static string ObjectCreated(Type type) => $"The '{type.Name}' successfully created.";
    }
}