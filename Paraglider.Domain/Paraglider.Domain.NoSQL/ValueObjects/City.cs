using MongoDB.Bson.Serialization.Attributes;

namespace Paraglider.Domain.NoSQL.ValueObjects;

public class City
{
    [BsonElement("id")]
    public long Id { get; set; }

    [BsonElement("name")]
    public string? Name { get; set; } = null!;
}
