using Paraglider.Clients.Gorko.Models.Abstractions;

namespace Paraglider.Clients.Gorko.Models;

public partial class User : IHaveId, IHaveReviews
{
    public long? Id { get; set; }

    public string? Name { get; set; }

    public RoleType Role { get; set; } = null!;

    public string? ProfileUrl { get; set; }

    public string? AvatarUrl { get; set; }

    public ICollection<CatalogMedia>? CatalogMedia { get; set; }

    public City? City { get; set; }

    public string? Text { get; set; }

    public ICollection<Contact>? Contacts { get; set; }
    
    public bool Banned { get; set; }

    public ICollection<Spec>? Specs { get; set; }

    public ICollection<Review>? Reviews { get; set; }
}