using Paraglider.AspNetCore.Identity.Infrastructure.Extensions;

namespace Paraglider.AspNetCore.Identity.Domain.ValueObjects
{
    public class Contacts
    {
        public Contacts() { }

        public Contacts(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                throw new ArgumentNullException("The phone number cannot be empty");
            }

            PhoneNumber = phoneNumber.ToPhoneNumberPattern();
        }

        public readonly string PhoneNumber;
    }
}
