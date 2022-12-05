using MongoDB.Bson.Serialization.Attributes;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.Domain.NoSQL.ValueObjects;

public class Album
{

    [BsonElement("name")]
    public string? Name { get; set; } = null!;

    [BsonElement("media")]
    public List<Media> Media { get; set; } = new List<Media>();
}