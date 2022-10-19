namespace Paraglider.AspNetCore.Identity.Domain
{
    public static partial class AppData
    {
        /// <summary>
        /// Common messages
        /// </summary>
        public static class Messages
        {
            public static string InvalidExternalAuthProvider(string provider) => $"Invalid external authorization provider: '{provider}'";
            public static string EmptyExternalLoginInfo() => "The ExternalLoginInfo was empty";
            public static string ExternalUserNotExist(string email, string externalId) => $"The user with email: '{email}' and externalId: '{externalId}' is not found. A new user will be created";
            public static string FailedCreatedExternalUser(string email, string externalId) => $"Error when creating a user from an external provider with email '{email}' and externalId '{externalId}'";
            public static string SuccessfullyCreatedExternalUser(string email, string externalId) => $"Successfully created a user from an external provider with email: '{email}' and externalId: '{externalId}'";
            public static string FailedAssignExternalLoginInfo(string email) => $"Failed to assign ExternalLoginInfo to user with email: '{email}'";
            public static string FailedAuthByExternalProvider(string email, string provider) => $"Failed to authorize user with email: '{email}' using external provider '{provider}'";
            public static string SuccessfullyAuthByExternalProvider(string email, string provider) => $"The user with email: '{email}' is successfully authorized using the external provider '{provider}'";
            public static string UserAlreadyAuthorized(string email) => $"User with email: {email} is already authorized";
            public static string BasicAuth_UserNotFound(string email) => $"Failed basic authorization. User with email: {email} was not found";
            public static string BasicAuth_WrongPassword(string email) => $"Failed basic authorization. Wrong password for account with email: '{email}'";
            public static string BasicAuth_SuccessfullyAuth(string email) => $"The user with email: '{email}' is successfully authorized";
            public static string FailedModelValidation(string modelName, string details) => $"Validation error '{modelName}' model. Details: '{details}'";
            public static string SuccessfullyLogOut(string email) => $"User with email: '{email}' logged out";
        }
    }
}