using MongoDB.Bson.Serialization.Attributes;

namespace Paraglider.Domain.NoSQL.ValueObjects;

public class Contacts
{
    [BsonElement("phoneNumber")]
    public ICollection<string>? PhoneNumbers { get; set; }

    [BsonIgnoreIfNull]
    [BsonElement("email")]
    public ICollection<string>? Emails { get; set; }
}