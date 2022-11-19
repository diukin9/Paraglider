using MongoDB.Bson.Serialization.Attributes;
using Paraglider.Infrastructure.Common.Extensions;

namespace Paraglider.Domain.NoSQL.ValueObjects;

public class Contacts
{
    [BsonElement("id")]
    public string Id { get; set; } = null!;

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

    public Contacts(string phoneNumber, string? email, string? telegram, string? whatsApp, string? viber)
    {
        if (string.IsNullOrEmpty(phoneNumber))
        {
            throw new ArgumentNullException("The phone number cannot be empty");
        }

        PhoneNumber = phoneNumber.ToPhoneNumberPattern();
        Email = email;
        Telegram = telegram;
        WhatsApp = whatsApp;
        Viber = viber;
    }
}