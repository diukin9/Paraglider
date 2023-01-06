using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Paraglider.Domain.NoSQL.Enums;
using Paraglider.Domain.NoSQL.ValueObjects;
using Paraglider.Infrastructure.Common.Interfaces;

namespace Paraglider.Domain.NoSQL.Entities;

[BsonIgnoreExtraElements]
public class Component : IIdentified<string>
{
    [BsonId, BsonElement("id")] 
    public string Id { get; set; } = null!;

    [BsonElement("externalId")] 
    public string ExternalId { get; set; } = null!;

    [BsonElement("provider")] 
    public string Provider { get; set; } = null!;

    [BsonElement("name")] 
    public string Name { get; set; } = null!;

    [BsonElement("description")] 
    public string? Description { get; set; } = null!;

    [BsonElement("avatarUrl")] 
    public string? AvatarUrl { get; set; }

    [BsonElement("category")] 
    public Category Category { get; set; } = null!;

    [BsonElement("city")] 
    public City City { get; set; } = null!;

    [BsonElement("album")] 
    public Album Album { get; set; } = null!; 

    [BsonElement("contacts")] 
    public Contacts Contacts { get; set; } = null!; 

    [BsonElement("reviews")] 
    public ICollection<Review> Reviews { get; set; } = new HashSet<Review>(); 

    [BsonElement("manufactureYear")] 
    public DateTime? ManufactureYear { get; set; } 

    [BsonElement("minRentLength")] 
    public TimeSpan? MinRentLength { get; set; } 

    [BsonElement("capacity")] 
    public int? Capacity { get; set; } 

    [BsonElement("services")] 
    public ICollection<Service>? Services { get; set; } 

    [BsonElement("halls")] 
    public ICollection<Hall>? Halls { get; set; } 

    [BsonElement("updatedAt")] 
    public DateTime UpdatedAt { get; set; }

    [BsonElement("status")] 
    public ComponentStatus Status { get; set; }

    [BsonElement("popularity")]
    public long Popularity { get; set; }

    [BsonElement("rating")]
    public double Rating
    {
        get => Reviews.Any() 
            ? Reviews.Sum(x => x.Evaluation) / Reviews.Count
            : 0.0;
    }
}