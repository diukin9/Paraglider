using MongoDB.Bson.Serialization.Attributes;
using Paraglider.Domain.NoSQL.Enums;
using Paraglider.Domain.NoSQL.ValueObjects;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.Domain.NoSQL.Entities;

public class WeddingComponent : IAggregateRoot
{
    [BsonId] public Guid Id { get; set; }
    public string Provider { get; set; } = null!;

    public WeddingComponentType Type { get; set; }

    public string Name { get; set; } = null!;
    public string? Description { get; set; } = null!;
    public string? AvatarUrl { get; set; }

    public DateTime? ManufactureYear { get; set; }
    public TimeInterval? MinRentLength { get; set; }
    public int? Capacity { get; set; }

    public Guid CityId { get; set; }

    public Album Album { get; set; } = null!;
    public Contacts Contacts { get; set; } = null!;

    public List<Review> Reviews { get; set; } = new List<Review>();
    public List<Service> Services { get; set; } = new List<Service>();
    public List<Hall> Premises { get; set; } = new List<Hall>();
}

