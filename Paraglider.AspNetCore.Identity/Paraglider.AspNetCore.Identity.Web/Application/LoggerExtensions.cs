namespace Paraglider.AspNetCore.Identity.Web.Application
{
    /// <summary>
    /// The number identifiers for events in the microservices
    /// </summary>
    internal static class EventNumbers
    {
        internal static readonly EventId BasicAuthorizeId = new(010001001, nameof(BasicAuthorizeId));
        internal static readonly EventId ValidationId = new(010001002, nameof(ValidationId));
    }

    /// <summary>
    /// Event logging as ILogger extension.
    /// </summary>
    internal static class LoggerExtensions
    {
        #region Authorize

        /// <summary>
        /// EventItem register action event
        /// </summary>
        /// <param name="source"></param>
        /// <param name="email"></param>
        /// <param name="exception"></param>
        internal static void MicroserviceBasicAuthorize(this ILogger source, string email, Exception? exception = null)
        {
            switch (exception)
            {
                case null:
                    BasicAuthorizeExecute(source, email, exception);
                    break;
                default:
                    BasicAuthorizeFailedExecute(source, email, exception);
                    break;
            }
        }

        private static readonly Action<ILogger, string, Exception?> BasicAuthorizeExecute =
            LoggerMessage.Define<string>(
                LogLevel.Information,
                EventNumbers.BasicAuthorizeId,
                "User with email: '{email}' is successfully authorized");
                

        private static readonly Action<ILogger, string, Exception?> BasicAuthorizeFailedExecute =
            LoggerMessage.Define<string>(
                LogLevel.Error, 
                EventNumbers.BasicAuthorizeId,
                "Failed to authorize user with email: '{email}'");

        #endregion

        #region Validation

        /// <summary>
        /// EventItem register action event
        /// </summary>
        /// <param name="source"></param>
        /// <param name="type"></param>
        /// <param name="exception"></param>
        internal static void MicroserviceValidation(this ILogger source, Type type, Exception exception)
        {
            ValidationFailed(source, type.Name, exception);
        }

        private static readonly Action<ILogger, string, Exception?> ValidationFailed =
            LoggerMessage.Define<string>(
                LogLevel.Error,
                EventNumbers.BasicAuthorizeId,
                "Model '{name}' validation error");

        #endregion
    }
}