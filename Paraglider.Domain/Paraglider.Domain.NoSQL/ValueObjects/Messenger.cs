using MongoDB.Bson.Serialization.Attributes;

namespace Paraglider.Domain.NoSQL.ValueObjects;

public class Messenger
{
    [BsonElement("key")]
    public string MessengerKey { get; set; } = null!;

    [BsonElement("identifier")]
    public string UserIdentifier { get; set; } = null!;
}
