using MongoDB.Bson.Serialization.Attributes;
using Paraglider.Domain.NoSQL.Enums;
using System.Text;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.Domain.NoSQL.ValueObjects;

public class HallRentalPrice
{
    [BsonElement("pricePerPerson")]
    public readonly decimal? PricePerPerson;

    [BsonElement("rentalPrice")]
    public readonly decimal? RentalPrice;

    public HallRentalPrice(decimal value, HallRentalPriceType type)
    {
        if (value < 0)
        {
            throw new ArgumentException(ExceptionMessages.CannotBeNegative(nameof(Price)));
        }

        switch (type)
        {
            case HallRentalPriceType.PricePerPerson:
                PricePerPerson = value;
                break;
            case HallRentalPriceType.RentalPrice:
                RentalPrice = value;
                break;
        }
    }

    public HallRentalPrice(decimal pricePerPerson, decimal rentalPrice)
    {
        if (pricePerPerson < 0 || rentalPrice < 0)
        {
            throw new ArgumentException(ExceptionMessages.CannotBeNegative(nameof(Price)));
        }

        PricePerPerson = pricePerPerson;
        RentalPrice = rentalPrice;
    }

    public override string ToString()
    {
        var builder = new StringBuilder();
        if (RentalPrice.HasValue)
        {
            builder.Append($"аренда {RentalPrice}");
        }
        if (RentalPrice.HasValue && PricePerPerson.HasValue)
        {
            builder.Append(" + ");
        }
        if (RentalPrice.HasValue)
        {
            builder.Append($"от {PricePerPerson} руб./чел.");
        }

        return builder.ToString();
    }
}
