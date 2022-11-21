using MongoDB.Bson.Serialization.Attributes;
using Paraglider.Infrastructure.Common.Extensions;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.Domain.NoSQL.ValueObjects;

public class Contacts
{
    [BsonElement("id")]
    public readonly string Id;

    [BsonElement("phoneNumber")]
    public readonly string PhoneNumber = null!;

    [BsonElement("email")]
    public readonly string? Email;

    [BsonElement("telegram")]
    public readonly string? Telegram;

    [BsonElement("whatsApp")]
    public readonly string? WhatsApp;

    [BsonElement("viber")]
    public readonly string? Viber;

    [BsonElement("vk")]
    public readonly string? Vkontakte;

    [BsonElement("instagram")]
    public readonly string? Instagram;

    public Contacts(
        string id,
        string phoneNumber,
        string? email = null,
        string? telegram = null,
        string? whatsApp = null,
        string? viber = null)
    {
        Id = id ?? throw new ArgumentNullException(
            ExceptionMessages.NullOrEmpty(nameof(id)));

        PhoneNumber = phoneNumber?.ToPhoneNumberPattern() 
            ?? throw new ArgumentNullException(
                ExceptionMessages.NullOrEmpty(nameof(phoneNumber)));

        Email = email;
        Telegram = telegram;
        WhatsApp = whatsApp;
        Viber = viber;
    }
}