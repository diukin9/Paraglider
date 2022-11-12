namespace Paraglider.Domain.Abstractions
{
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
}