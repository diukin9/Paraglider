namespace Paraglider.Infrastructure.Common;

public static partial class AppData
{
    public static class Messages
    {
        public const string SuccessfulExternalAuth = "The user is successfully authorized using the external provider.";
        public const string SuccessfulAuth = "The user is successfully authorized.";
        public const string SuccessfulLogout = "User logged out.";
        public const string SuccessfulRegistration = "User is successfully registered";
        public const string SuccessfulEmailConfirmation = "Email was successfully confirmed";
        public const string PasswordSuccesfullyChanged = "Password was successfully changed";
        
        public static string ObjectFound(Type type) => $"The '{type.Name}' object successfully found.";
        public static string ObjectCreated(Type type) => $"The '{type.Name}' successfully created.";
        public static string ObjectCreated(string name) => $"The '{name}' successfully created.";
        public static string ObjectUpdated(string name) => $"The '{name}' successfully updated.";

        public static string ConfirmationEmailSent(string mail)
        {
            return $"Confirmation mail was successfully sent on {mail}";
        }
    }
}