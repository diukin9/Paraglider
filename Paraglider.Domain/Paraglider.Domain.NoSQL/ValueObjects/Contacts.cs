using MongoDB.Bson.Serialization.Attributes;
using Paraglider.Infrastructure.Common.Extensions;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.Domain.NoSQL.ValueObjects;

public class Contacts
{
    [BsonElement("phoneNumber")]
    public string PhoneNumber { get; set; } = null!;

    [BsonIgnoreIfNull]
    [BsonElement("email")]
    public string? Email { get; set; }

    [BsonIgnoreIfNull]
    [BsonElement("telegram")]
    public string? Telegram { get; set; }

    [BsonIgnoreIfNull]
    [BsonElement("whatsApp")]
    public string? WhatsApp { get; set; }

    [BsonIgnoreIfNull]
    [BsonElement("viber")]
    public string? Viber { get; set; }

    [BsonIgnoreIfNull]
    [BsonElement("vk")]
    public string? Vkontakte { get; set; }

    [BsonIgnoreIfNull]
    [BsonElement("instagram")]
    public string? Instagram { get; set; }

    public Contacts(
        string phoneNumber,
        string? email = null,
        string? telegram = null,
        string? whatsApp = null,
        string? viber = null)
    {
        PhoneNumber = phoneNumber?.ToPhoneNumberPattern() 
            ?? throw new ArgumentNullException(
                ExceptionMessages.NullOrEmpty(nameof(phoneNumber)));

        Email = email;
        Telegram = telegram;
        WhatsApp = whatsApp;
        Viber = viber;
    }
}