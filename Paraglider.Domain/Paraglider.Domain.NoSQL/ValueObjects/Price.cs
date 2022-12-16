using MongoDB.Bson.Serialization.Attributes;

namespace Paraglider.Domain.NoSQL.ValueObjects;

public class Price
{
    [BsonIgnoreIfNull]
    [BsonElement("min")]
    public decimal? Min { get; set; }

    [BsonIgnoreIfNull]
    [BsonElement("max")]
    public decimal? Max { get; set; }
}
