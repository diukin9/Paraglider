using MongoDB.Bson.Serialization.Attributes;

namespace Paraglider.Domain.NoSQL.ValueObjects;

public class Album
{
    [BsonElement("media")]
    public List<Media> Media { get; set; } = new List<Media>();
}