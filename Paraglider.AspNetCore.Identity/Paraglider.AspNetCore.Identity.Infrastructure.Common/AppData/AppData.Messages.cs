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
            /// <param name="email"></param>
            /// <param name="externalId"></param>
            /// <returns></returns>
            public static string ExternalAuth_UserNotExist(string email, string externalId) => $"The user with email: '{email}' and externalId: '{externalId}' is not found. A new user will be created";

            /// <summary>
            /// Error when creating a user from an external provider
            /// </summary>
            /// <param name="email"></param>
            /// <param name="externalId"></param>
            /// <returns></returns>
            public static string ExternalAuth_UserNotCreated(string email, string externalId) => $"Error when creating a user from an external provider with email '{email}' and externalId '{externalId}'";

            /// <summary>
            /// Successfully created a user from an external provider
            /// </summary>
            /// <param name="email"></param>
            /// <param name="externalId"></param>
            /// <returns></returns>
            public static string ExternalAuth_UserCreated(string email, string externalId) => $"Successfully created a user from an external provider with email: '{email}' and externalId: '{externalId}'";

            /// <summary>
            /// Failed to assign ExternalLoginInfo to user
            /// </summary>
            /// <param name="email"></param>
            /// <returns></returns>
            public static string ExternalAuth_FailedAssignExternalLoginInfo(string email) => $"Failed to assign ExternalLoginInfo to user with email: '{email}'";

            /// <summary>
            /// Failed to authorize user
            /// </summary>
            /// <param name="email"></param>
            /// <param name="provider"></param>
            /// <returns></returns>
            public static string ExternalAuth_FailedAuth(string email, string provider) => $"Failed to authorize user with email: '{email}' using external provider '{provider}'";

            /// <summary>
            /// The user is successfully authorized
            /// </summary>
            /// <param name="email"></param>
            /// <param name="provider"></param>
            /// <returns></returns>
            public static string ExternalAuth_SuccessfullAuth(string email, string provider) => $"The user with email: '{email}' is successfully authorized using the external provider '{provider}'";

            #endregion

            #region Auth

            /// <summary>
            /// User is already authorized
            /// </summary>
            /// <param name="email"></param>
            /// <returns></returns>
            public static string Auth_UserAlreadyAuthorized(string email) => $"User with email: {email} is already authorized";

            #endregion Auth

            #region BasicAuth

            /// <summary>
            /// Failed basic authorization. User was not found
            /// </summary>
            /// <param name="email"></param>
            /// <returns></returns>
            public static string BasicAuth_UserNotFound(string email) => $"Failed basic authorization. User with email: {email} was not found";

            /// <summary>
            /// Failed basic authorization. Wrong password for account
            /// </summary>
            /// <param name="email"></param>
            /// <returns></returns>
            public static string BasicAuth_WrongPassword(string email) => $"Failed basic authorization. Wrong password for account with email: '{email}'";

            /// <summary>
            /// The user is successfully authorized
            /// </summary>
            /// <param name="email"></param>
            /// <returns></returns>
            public static string BasicAuth_SuccessfulAuth(string email) => $"The user with email: '{email}' is successfully authorized";

            #endregion

            #region LogOut

            /// <summary>
            /// User logged out
            /// </summary>
            /// <param name="email"></param>
            /// <returns></returns>
            public static string LogOut_SuccessfulLogOut(string email) => $"User with email: '{email}' logged out";

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
        }
    }
}