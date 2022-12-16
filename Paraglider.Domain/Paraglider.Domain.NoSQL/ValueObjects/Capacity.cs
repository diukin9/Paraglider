using MongoDB.Bson.Serialization.Attributes;

namespace Paraglider.Domain.NoSQL.ValueObjects;

public class Capacity
{
    [BsonElement("min")]
    public int? Min { get; set; }

    [BsonElement("max")]
    public int? Max { get; set; }
}