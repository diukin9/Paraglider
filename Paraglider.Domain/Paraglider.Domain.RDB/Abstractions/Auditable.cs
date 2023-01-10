using Paraglider.Infrastructure.Common.Interfaces;

namespace Paraglider.Domain.RDB.Abstractions;

/// <summary>
/// Represents 'Audit-able' table from the Property Database
/// </summary>
public abstract class Auditable : IIdentified, IAuditable
{
    public Guid Id { get; set; }

    /// <summary>
    /// DateTime when entity created.
    /// It's never changed
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// User name who created entity.
    /// It's never changed
    /// </summary>
    public string CreatedBy { get; set; } = null!;

    /// <summary>
    /// Last date entity updated
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Author of last updated
    /// </summary>
    public string UpdatedBy { get; set; } = null!;
}