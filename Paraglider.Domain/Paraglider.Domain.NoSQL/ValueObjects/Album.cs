using MongoDB.Bson.Serialization.Attributes;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.Domain.NoSQL.ValueObjects;

public class Album
{
    [BsonElement("id")]
    public string Id { get; set; } = null!;

    [BsonElement("name")]
    public string? Name { get; set; } = null!;

    [BsonElement("media")]
    public List<Media> Media { get; set; } = new List<Media>();

    public Album(string id, string? name = null, List<Media>? media = null)
    {
        Id = id ?? throw new ArgumentNullException(ExceptionMessages.NullOrEmpty(nameof(id)));
        Media = media ?? new List<Media>();
        Name = name;
    }
}