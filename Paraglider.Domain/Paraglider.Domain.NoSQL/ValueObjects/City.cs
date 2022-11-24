using MongoDB.Bson.Serialization.Attributes;

namespace Paraglider.Domain.NoSQL.ValueObjects;

public class City
{
    [BsonElement("id")]
    public string Id { get; set; } = null!;

    [BsonElement("name")]
    public string? Name { get; set; } = null!;
}
