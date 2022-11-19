using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Paraglider.Domain.NoSQL.Enums;
using Paraglider.Domain.NoSQL.ValueObjects;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.Domain.NoSQL.Entities;

[BsonIgnoreExtraElements]
public class WeddingComponent : IAggregateRoot, IIdentified
{
    [BsonId]
    [BsonElement("id")]
    public Guid Id { get; set; }

    [BsonElement("provider")]
    public string Provider { get; set; } = null!;

    [BsonElement("componentType")]
    public WeddingComponentType Type { get; set; }

    [BsonElement("name")]
    public string Name { get; set; } = null!;

    [BsonElement("description")]
    public string? Description { get; set; } = null!;

    [BsonElement("avatarUrl")]
    public string? AvatarUrl { get; set; }

    [BsonElement("manufactureYear")]
    public DateTime? ManufactureYear { get; set; }

    [BsonElement("minRentLength")]
    public TimeSpan? MinRentLength { get; set; }

    [BsonElement("capacity")]
    public int? Capacity { get; set; }

    [BsonElement("cityId")]
    public Guid CityId { get; set; }

    [BsonElement("album")]
    public Album Album { get; set; } = null!;

    [BsonElement("contacts")]
    public Contacts Contacts { get; set; } = null!;

    [BsonElement("reviews")]
    public List<Review> Reviews { get; set; } = new List<Review>();

    [BsonElement("services")]
    public List<Service> Services { get; set; } = new List<Service>();

    [BsonElement("premises")]
    public List<Hall> Halls { get; set; } = new List<Hall>();
}

