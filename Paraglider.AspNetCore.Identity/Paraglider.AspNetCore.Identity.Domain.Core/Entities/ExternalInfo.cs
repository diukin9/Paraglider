using Paraglider.AspNetCore.Identity.Domain.Enums;
using Paraglider.AspNetCore.Identity.Domain.ValueObjects.Abstractions;

namespace Paraglider.AspNetCore.Identity.Domain.Entities
{
    /// <summary>
    /// External login information
    /// </summary>
    public class ExternalInfo : IHaveId
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
