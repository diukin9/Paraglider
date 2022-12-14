using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Paraglider.Domain.NoSQL.ValueObjects;
using Paraglider.Infrastructure.Common.Abstractions;
using Paraglider.Infrastructure.Common.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Paraglider.Domain.NoSQL.Entities;

[BsonIgnoreExtraElements]
public class Component : IIdentified<string>
{
    [BsonId, BsonElement("id")]
    [Required]
    public string Id { get; set; } = null!;

    [BsonElement("externalId")]
    [Required]
    public string ExternalId { get; set; } = null!;

    [Required, IsEnumName(typeof(Common.Enums.Source))]
    [BsonElement("provider")]
    public string Provider { get; set; } = null!;

    [BsonElement("category")]
    [Required]
    public Category Category { get; set; } = null!;

    [BsonElement("name")]
    [Required]
    public string Name { get; set; } = null!;

    [BsonElement("description")]
    public string? Description { get; set; } = null!;

    [BsonElement("avatarUrl")]
    [Required]
    public string? AvatarUrl { get; set; }

    [BsonElement("city")]
    [Required]
    public City City { get; set; } = null!;

    [BsonElement("album")]
    public Album Album { get; set; } = null!; 

    [BsonElement("contacts")]
    [Required]
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
}