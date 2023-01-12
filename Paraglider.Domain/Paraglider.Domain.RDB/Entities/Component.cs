using Paraglider.Domain.RDB.Enums;
using Paraglider.Infrastructure.Common.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paraglider.Domain.RDB.Entities;

public class Component : IAggregateRoot
{
    public Guid Id { get; set; }

    public string ExternalId { get; set; } = null!;

    public string Provider { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; } = null!;

    public string? AvatarUrl { get; set; }

    public Guid CategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public Guid CityId { get; set; }

    public virtual City City { get; set; } = null!;

    public Guid? AlbumId { get; set; }

    public virtual Album? Album { get; set; } = null!; 

    public virtual ICollection<Contact> Contacts { get; set; } = new HashSet<Contact>(); 

    public virtual ICollection<Review> Reviews { get; set; } = new HashSet<Review>();

    public DateTime? ManufactureYear { get; set; }

    public TimeSpan? MinRentLength { get; set; }

    public int? Capacity { get; set; } 

    public virtual ICollection<Service>? Services { get; set; } 

    public virtual ICollection<Hall>? Halls { get; set; }

    public DateTime UpdatedAt { get; set; }

    public ComponentStatus Status { get; set; }

    public long Popularity { get; set; }

    public double Rating { get; set; }
}