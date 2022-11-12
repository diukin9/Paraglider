﻿using Microsoft.AspNetCore.Identity;
using Paraglider.Domain.Abstractions;

namespace Paraglider.Domain.Entities
{
    /// <summary>
    /// Default user for application.
    /// Add profile data for application users by adding properties to the ApplicationUser class
    /// </summary>
    public class ApplicationUser : IdentityUser<Guid>
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
        public virtual List<ExternalInfo> ExternalAuthInfo { get; set; } = new List<ExternalInfo>();

        public virtual List<WeddingPlanning> WeddingPlannings { get; set; } = new List<WeddingPlanning>();
    }
}