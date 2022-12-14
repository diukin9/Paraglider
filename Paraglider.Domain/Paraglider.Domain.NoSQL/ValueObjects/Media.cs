using MongoDB.Bson.Serialization.Attributes;
using Paraglider.Domain.NoSQL.Enums;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.Domain.NoSQL.ValueObjects;

public class Media
{
    [BsonElement("type")]
    public MediaType Type { get; set; }

    [BsonElement("url")]
    public string Url { get; set; } = null!;
}