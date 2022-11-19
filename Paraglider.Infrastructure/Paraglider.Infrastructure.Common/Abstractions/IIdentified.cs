namespace Paraglider.Infrastructure.Common.Abstractions;

/// <summary>
/// Identifier common interface
/// </summary>
public interface IIdentified
{
    /// <summary>
    /// Identifier
    /// </summary>
    Guid Id { get; set; }
}