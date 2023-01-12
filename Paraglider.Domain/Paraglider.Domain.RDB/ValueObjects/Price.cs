using System.ComponentModel.DataAnnotations.Schema;

namespace Paraglider.Domain.RDB.ValueObjects;

public class Price
{
    public decimal? Min { get; set; }

    public decimal? Max { get; set; }
}
