using MongoDB.Bson.Serialization.Attributes;
using Paraglider.Domain.NoSQL.Enums;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.Domain.NoSQL.ValueObjects;

public class Media
{
    [BsonElement("id")]
    public string Id { get; set; } = null!;

    [BsonElement("type")]
    public MediaType Type { get; set; }

    [BsonElement("url")]
    public string Url { get; set; } = null!;

    public Media(string id, MediaType type, string url)
    {
        Id = id ?? throw new ArgumentNullException(ExceptionMessages.NullOrEmpty(nameof(id)));
        Type = type;
        Url = url ?? throw new ArgumentNullException(ExceptionMessages.NullOrEmpty(nameof(url)));
    }
}