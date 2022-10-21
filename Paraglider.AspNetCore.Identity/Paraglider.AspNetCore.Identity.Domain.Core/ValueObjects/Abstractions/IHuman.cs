namespace Paraglider.AspNetCore.Identity.Domain.ValueObjects.Abstractions
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

        /// <summary>
        /// User second name
        /// </summary>
        public string? SecondName { get; set; }
    }
}
