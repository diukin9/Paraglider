using Paraglider.Domain.Abstractions;
using Paraglider.Domain.Enums;

namespace Paraglider.Domain.Entities
{
    /// <summary>
    /// External login information
    /// </summary>
    public class ExternalInfo : IIdentified
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// External Identifier
        /// </summary>
        public string ExternalId { get; set; } = null!;

        /// <summary>
        /// External provider
        /// </summary>
        public ExternalAuthProvider ExternalProvider { get; set; }
    }
}
