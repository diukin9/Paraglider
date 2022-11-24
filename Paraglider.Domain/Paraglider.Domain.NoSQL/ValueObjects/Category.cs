using MongoDB.Bson.Serialization.Attributes;

namespace Paraglider.Domain.NoSQL.ValueObjects;

public class Category
{
    [BsonElement("id")]
    public Guid Id { get; set; }

    [BsonElement("name")]
    public string Name { get; set; } = null!;
}
