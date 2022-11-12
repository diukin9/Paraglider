namespace Paraglider.Infrastructure
{
    /// <summary>
    /// Static data container
    /// </summary>
    public static partial class AppData
    {
        /// <summary>
        /// Current service name
        /// </summary>
        public const string ServiceName = "Paraglider";

        /// <summary>
        /// Template for creating the username of an external user
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="externalId"></param>
        /// <returns></returns>
        public static string ExternalUsernameTemplate(string? provider, string? externalId)
        {
            if (string.IsNullOrEmpty(provider))
            {
                throw new ArgumentNullException(nameof(provider));
            }
            else if (string.IsNullOrEmpty(externalId))
            {
                throw new ArgumentNullException(nameof(externalId));
            }
            return $"{provider}-{externalId}";
        }
    }
}