namespace Paraglider.AspNetCore.Identity.Domain.Abstractions
{
    /// <summary>
    /// The interface of the human properties
    /// </summary>
    public interface IHuman
    {
        /// <summary>
        /// User surname
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// User first name
        /// </summary>
        public string FirstName { get; set; }
    }
}
