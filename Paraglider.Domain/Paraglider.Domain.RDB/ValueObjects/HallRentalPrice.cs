using System.ComponentModel.DataAnnotations.Schema;

namespace Paraglider.Domain.RDB.ValueObjects;

public class HallPrice
{
    public decimal? PricePerPerson { get; set; }

    public decimal? RentalPrice { get; set; }
}
