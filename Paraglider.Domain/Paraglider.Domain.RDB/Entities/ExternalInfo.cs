using Paraglider.Domain.RDB.Abstractions;
using Paraglider.Domain.RDB.Enums;

namespace Paraglider.Domain.RDB.Entities;

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
