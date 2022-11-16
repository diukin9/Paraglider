using Paraglider.Infrastructure.Extensions;

namespace Paraglider.Domain.ValueObjects;

public class Contacts
{
    public readonly string PhoneNumber = null!;
    public readonly string? Email;
    public readonly string? Telegram;
    public readonly string? WhatsApp;
    public readonly string? Viber;
    public readonly string? Vkontakte;
    public readonly string? Instagram;

    //for entity framework
    private Contacts() { }

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
