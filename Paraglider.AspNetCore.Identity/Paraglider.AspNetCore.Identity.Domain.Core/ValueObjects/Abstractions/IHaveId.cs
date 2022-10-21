namespace Paraglider.AspNetCore.Identity.Domain.ValueObjects.Abstractions
{
    /// <summary>
    /// Identifier common interface
    /// </summary>
    public interface IHaveId
    {
        /// <summary>
        /// Identifier
        /// </summary>
        Guid Id { get; set; }
    }
}