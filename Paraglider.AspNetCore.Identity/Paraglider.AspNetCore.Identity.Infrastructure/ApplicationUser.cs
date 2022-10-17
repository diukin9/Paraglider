using Microsoft.AspNetCore.Identity;

namespace Paraglider.AspNetCore.Identity.Infrastructure
{
    /// <summary>
    /// Default user for application.
    /// Add profile data for application users by adding properties to the ApplicationUser class
    /// </summary>
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
    }
}