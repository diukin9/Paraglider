using Microsoft.AspNetCore.Identity;
using Paraglider.Infrastructure.Common.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paraglider.Domain.RDB.Entities;

public class ApplicationUser : IdentityUser<Guid>, IAggregateRoot
{
    public string? RefreshToken { get; set; }

    public DateTime? RefreshTokenExpiryTime { get; set; }

    public string FirstName { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public Guid CityId { get; set; }

    public virtual City City { get; set; } = null!;

    public Guid PlanningId { get; set; }

    public virtual Planning Planning { get; set; } = null!;

    public virtual ICollection<Component> Favourites { get; set; } = new HashSet<Component>();

    public virtual ICollection<ExtlAuthInfo> ExternalAuthInfo { get; set; } = new HashSet<ExtlAuthInfo>();
}