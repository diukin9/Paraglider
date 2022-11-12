using Paraglider.AspNetCore.Identity.Infrastructure.Extensions;

namespace Paraglider.AspNetCore.Identity.Infrastructure
{
    public static partial class AppData
    {
        /// <summary>
        /// Common messages
        /// </summary>
        public static class Messages
        {
            #region ExternalAuth

            /// <summary>
            /// Invalid external authorization provider
            /// </summary>
            /// <param name="provider"></param>
            /// <returns></returns>
            public static string ExternalAuth_InvalidProvider(string provider) => $"Invalid external authorization provider: '{provider}'";

            /// <summary>
            /// The ExternalLoginInfo was empty
            /// </summary>
            /// <returns></returns>
            public static string ExternalAuth_EmptyExternalLoginInfo() => "The ExternalLoginInfo was empty";

            /// <summary>
            /// The user is not found. A new user will be created
            /// </summary>
            /// <param name="login"></param>
            /// <param name="externalId"></param>
            /// <returns></returns>
            public static string ExternalAuth_UserNotExist(string login, string externalId) => $"The user with {(login.IsEmail() ? "email" : "username")}: '{login}' and externalId: '{externalId}' is not found. A new user will be created";

            /// <summary>
            /// Error when creating a user from an external provider
            /// </summary>
            /// <param name="login"></param>
            /// <param name="externalId"></param>
            /// <returns></returns>
            public static string ExternalAuth_UserNotCreated(string login, string externalId) => $"Error when creating a user from an external provider with {(login.IsEmail() ? "email" : "username")}: '{login}' and externalId '{externalId}'";

            /// <summary>
            /// Successfully created a user from an external provider
            /// </summary>
            /// <param name="login"></param>
            /// <param name="externalId"></param>
            /// <returns></returns>
            public static string ExternalAuth_UserCreated(string login, string externalId) => $"Successfully created a user from an external provider with {(login.IsEmail() ? "email" : "username")}: '{login}' and externalId: '{externalId}'";

            /// <summary>
            /// Failed to assign ExternalLoginInfo to user
            /// </summary>
            /// <param name="login"></param>
            /// <returns></returns>
            public static string ExternalAuth_FailedAssignExternalLoginInfo(string login) => $"Failed to assign ExternalLoginInfo to user with {(login.IsEmail() ? "email" : "username")}: '{login}'";

            /// <summary>
            /// Failed to authorize user
            /// </summary>
            /// <param name="login"></param>
            /// <param name="provider"></param>
            /// <returns></returns>
            public static string ExternalAuth_FailedAuth(string login, string provider) => $"Failed to authorize user with {(login.IsEmail() ? "email" : "username")}: '{login}' using external provider '{provider}'";

            /// <summary>
            /// The user is successfully authorized
            /// </summary>
            /// <param name="login"></param>
            /// <param name="provider"></param>
            /// <returns></returns>
            public static string ExternalAuth_SuccessfullAuth(string login, string provider) => $"The user with {(login.IsEmail() ? "email" : "username")}: '{login}' is successfully authorized using the external provider '{provider}'";

            #endregion

            #region Auth

            /// <summary>
            /// User is already authorized
            /// </summary>
            /// <param name="username"></param>
            /// <returns></returns>
            public const string Auth_NoAuthorizedUser = "There is no valid authorized user";

            #endregion Auth

            #region BasicAuth

            /// <summary>
            /// Failed basic authorization. User was not found
            /// </summary>
            /// <param name="login"></param>
            /// <returns></returns>
            public static string BasicAuth_UserNotFound(string login) => $"Failed basic authorization. User with {(login.IsEmail() ? "email" : "username")}: {login} was not found";

            /// <summary>
            /// Failed basic authorization. Wrong password for account
            /// </summary>
            /// <param name="login"></param>
            /// <returns></returns>
            public static string BasicAuth_WrongPassword(string login) => $"Failed basic authorization. Wrong password for account with {(login.IsEmail() ? "email" : "username")}: '{login}'";

            /// <summary>
            /// The user is successfully authorized
            /// </summary>
            /// <param name="login"></param>
            /// <returns></returns>
            public static string BasicAuth_SuccessfulAuth(string login) => $"The user with {(login.IsEmail() ? "email" : "username")}: '{login}' is successfully authorized";

            /// <summary>
            /// Failed to authorize user
            /// </summary>
            /// <param name="login"></param>
            /// <returns></returns>
            public static string BasicAuth_FailedAuth(string login) => $"Failed to authorize user with {(login.IsEmail() ? "email" : "username")}: '{login}'";

            #endregion

            #region LogOut

            /// <summary>
            /// User logged out
            /// </summary>
            /// <param name="email"></param>
            /// <returns></returns>
            public static string LogOut_SuccessfulLogOut(string username) => $"User with {(username.IsEmail() ? "email" : "username")}: '{username}' logged out";

            #endregion

            #region Validation

            /// <summary>
            /// Validation error with details
            /// </summary>
            /// <param name="type"></param>
            /// <param name="details"></param>
            /// <returns></returns>
            public static string Validation_FailedValidation(Type type, string details) => $"Validation error '{type.Name}' model. Details: '{details}'";

            #endregion

            /// <summary>
            /// User not found
            /// </summary>
            public const string UserNotFound = "User not found";

            /// <summary>
            /// ... was empty
            /// </summary>
            public static string EmptyObject(string typeName) => $"'{typeName}' was empty";
        }
    }
}