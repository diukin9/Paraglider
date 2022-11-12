﻿using Microsoft.AspNetCore.Identity;
using Paraglider.AspNetCore.Identity.Domain.Abstractions;

namespace Paraglider.AspNetCore.Identity.Domain.Entities
{
    /// <summary>
    /// Default user for application.
    /// Add profile data for application users by adding properties to the ApplicationUser class
    /// </summary>
    public class ApplicationUser : IdentityUser<Guid>, IHuman
    {
        /// <summary>
        /// User first name
        /// </summary>
        public string FirstName { get; set; } = null!;

        /// <summary>
        /// User surname
        /// </summary>
        public string Surname { get; set; } = null!;

        public Guid CityId { get; set; }
        public virtual City City { get; set; } = null!;

        /// <summary>
        /// External login information
        /// </summary>
        public virtual List<TempExternalInfo> ExternalInfo { get; set; } = new List<TempExternalInfo>();

        public virtual List<WeddingPlan> Plans { get; set; } = new List<WeddingPlan>();
    }
}