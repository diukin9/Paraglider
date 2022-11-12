using Paraglider.AspNetCore.Identity.Domain.Abstractions;
using Paraglider.AspNetCore.Identity.Domain.Enums;

namespace Paraglider.AspNetCore.Identity.Domain.Entities
{
    /// <summary>
    /// External login information
    /// </summary>
    public class TempExternalInfo : IHaveId
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
