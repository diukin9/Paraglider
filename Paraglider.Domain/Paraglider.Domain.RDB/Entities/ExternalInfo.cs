using Paraglider.Domain.RDB.Enums;
using Paraglider.Infrastructure.Common.Abstractions;

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
