using Paraglider.Domain.NoSQL.Enums;
using System.Text;

namespace Paraglider.Domain.NoSQL.ValueObjects;

public class HallRentalPrice
{
    public readonly decimal? PricePerPerson;
    public readonly decimal? RentalPrice;

    //for entity framework
    private HallRentalPrice() { }

    public HallRentalPrice(decimal value, HallRentalPriceType type)
    {
        if (value < 0)
        {
            throw new ArgumentException("Price cannot be negative");
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
        if (pricePerPerson <= 0 || rentalPrice <= 0)
        {
            throw new ArgumentException("Price cannot be negative");
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
