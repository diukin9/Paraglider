using Paraglider.Infrastructure.Extensions;

namespace Paraglider.Infrastructure;

public static partial class AppData
{
    /// <summary>
    /// Common messages
    /// </summary>
    public static class Messages
    {
        #region ExternalAuth

        public static string ExternalAuth_InvalidProvider(string provider) => $"Invalid external authorization provider: '{provider}'";
        public static string ExternalAuth_EmptyExternalLoginInfo() => "The ExternalLoginInfo was empty";
        public static string ExternalAuth_UserNotExist(string login, string externalId) => $"The user with {(login.IsEmail() ? "email" : "username")}: '{login}' and externalId: '{externalId}' is not found. A new user will be created";
        public static string ExternalAuth_UserNotCreated(string login, string externalId) => $"Error when creating a user from an external provider with {(login.IsEmail() ? "email" : "username")}: '{login}' and externalId '{externalId}'";
        public static string ExternalAuth_UserCreated(string login, string externalId) => $"Successfully created a user from an external provider with {(login.IsEmail() ? "email" : "username")}: '{login}' and externalId: '{externalId}'";
        public static string ExternalAuth_FailedAssignExternalLoginInfo(string login) => $"Failed to assign ExternalLoginInfo to user with {(login.IsEmail() ? "email" : "username")}: '{login}'";
        public static string ExternalAuth_FailedAuth(string login, string provider) => $"Failed to authorize user with {(login.IsEmail() ? "email" : "username")}: '{login}' using external provider '{provider}'";
        public static string ExternalAuth_SuccessfullAuth(string login, string provider) => $"The user with {(login.IsEmail() ? "email" : "username")}: '{login}' is successfully authorized using the external provider '{provider}'";

        #endregion

        #region Auth

        public const string Auth_NoAuthorizedUser = "There is no valid authorized user";

        #endregion Auth

        #region BasicAuth

        public static string BasicAuth_UserNotFound(string login) => $"Failed basic authorization. User with {(login.IsEmail() ? "email" : "username")}: {login} was not found";
        public static string BasicAuth_WrongPassword(string login) => $"Failed basic authorization. Wrong password for account with {(login.IsEmail() ? "email" : "username")}: '{login}'";
        public static string BasicAuth_SuccessfulAuth(string login) => $"The user with {(login.IsEmail() ? "email" : "username")}: '{login}' is successfully authorized";
        public static string BasicAuth_FailedAuth(string login) => $"Failed to authorize user with {(login.IsEmail() ? "email" : "username")}: '{login}'";

        #endregion

        #region LogOut

        public static string LogOut_SuccessfulLogOut(string username) => $"User with {(username.IsEmail() ? "email" : "username")}: '{username}' logged out";

        #endregion

        #region Validation

        public static string Validation_FailedValidation(Type type, string details) => $"Validation error '{type.Name}' model. Details: '{details}'";

        #endregion

        public const string UserNotFound = "User not found";
        public static string EmptyObject(string typeName) => $"'{typeName}' was empty";
    }
}