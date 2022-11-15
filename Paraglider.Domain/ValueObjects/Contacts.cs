﻿using Paraglider.Infrastructure.Extensions;

namespace Paraglider.Domain.ValueObjects;

public class Contacts
{
    public Contacts() 
    {
        throw new NotImplementedException();
    }

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
