namespace Paraglider.Infrastructure.Common;

public static partial class AppData
{
    public static class Messages
    {
        public const string SuccessfullExternalAuth = "The user is successfully authorized using the external provider.";
        public const string SuccessfulAuth = "The user is successfully authorized.";
        public const string SuccessfulLogout = "User logged out.";
        public static string ObjectCreated(string name) => $"The '{name}' successfully created.";
        public static string ObjectUpdated(string name) => $"The '{name}' successfully updated.";
    }
}