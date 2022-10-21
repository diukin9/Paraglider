using Microsoft.AspNetCore.Identity;
using Paraglider.AspNetCore.Identity.Domain.ValueObjects.Abstractions;

namespace Paraglider.AspNetCore.Identity.Domain.Entities
{
    /// <summary>
    /// Default user for application.
    /// Add profile data for application users by adding properties to the ApplicationUser class
    /// </summary>
    public class ApplicationUser : IdentityUser<Guid>, IHuman
    {
        /// <summary>
        /// User surname
        /// </summary>
        public string Surname { get; set; } = null!;

        /// <summary>
        /// User first name
        /// </summary>
        public string FirstName { get; set; } = null!;

        /// <summary>
        /// User second name
        /// </summary>
        public string? SecondName { get; set; }

        /// <summary>
        /// External login information
        /// </summary>
        public virtual List<ExternalInfo> ExternalInfo { get; set; } = new List<ExternalInfo>();

        /// <summary>
        /// Generator of the full user name depending on the presence of a second name
        /// </summary>
        public string FullName
        {
            get
            {
                if (string.IsNullOrEmpty(SecondName))
                {
                    return $"{Surname} {FirstName}";
                }
                return $"{Surname} {FirstName} {SecondName}";
            }
        }

        /// <summary>
        /// Generator of the short user name depending on the presence of a second name
        /// </summary>
        public string ShortName
        {
            get
            {
                if (string.IsNullOrEmpty(SecondName))
                {
                    return $"{FirstName}";
                }
                return $"{FirstName} {SecondName}";
            }
        }
    }
}