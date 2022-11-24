using Microsoft.AspNetCore.Identity;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.Domain.RDB.Entities;

public class ApplicationUser : IdentityUser<Guid>, IAggregateRoot
{
    public string FirstName { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public Guid CityId { get; set; }
    public virtual City City { get; set; } = null!;

    public Guid PlanningId { get; set; }
    public virtual Planning Planning { get; set; } = null!;

    public virtual List<UserComponent> Favourites { get; set; } = new List<UserComponent>();
    public virtual List<ExternalAuthInfo> ExternalAuthInfo { get; set; } = new List<ExternalAuthInfo>();
}