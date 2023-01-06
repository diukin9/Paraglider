using MongoDB.Bson.Serialization.Attributes;

namespace Paraglider.Domain.NoSQL.ValueObjects;

public class Contacts
{
    [BsonElement("phoneNumber")]
    public ICollection<string>? PhoneNumbers { get; set; }

    [BsonElement("email")]
    public ICollection<string>? Emails { get; set; }

    [BsonElement("messengers")]
    public ICollection<string>? Messengers { get; set; }
}