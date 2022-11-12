using System.Text;

namespace Paraglider.Domain.ValueObjects
{
    public class PremiseRentalPrice
    {
        public readonly decimal? PricePerPerson;
        public readonly decimal? RentalPrice;

        public PremiseRentalPrice() { }

        public PremiseRentalPrice(decimal? pricePerPerson, decimal? rentalPrice)
        {
            if (!pricePerPerson.HasValue && !rentalPrice.HasValue)
            {
                throw new ArgumentException("Empty parameters were passed");
            }

            if (pricePerPerson.HasValue && pricePerPerson.Value <= 0 || rentalPrice.HasValue && rentalPrice.Value <= 0)
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
}
