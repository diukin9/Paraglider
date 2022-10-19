using Microsoft.AspNetCore.Identity;

namespace Paraglider.AspNetCore.Identity.Infrastructure.Data
{
    /// <summary>
    /// Default user for application.
    /// Add profile data for application users by adding properties to the ApplicationUser class
    /// </summary>
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string? SecondName { get; set; }

        public virtual List<ExternalInfo> ExternalInfo { get; set; }

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