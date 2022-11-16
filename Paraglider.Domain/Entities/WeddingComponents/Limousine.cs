using Paraglider.Domain.Abstractions;
using Paraglider.Infrastructure.Abstractions;

namespace Paraglider.Domain.Entities;

public class Limousine : WeddingComponent, IAggregateRoot
{
    public DateTime? ManufactureYear { get; set; }
    public TimeSpan? MinRentLength { get; set; }

    private int? capacity;
    public int? Capacity 
    { 
        get => capacity; 
        set
        {
            if (value.HasValue && value.Value < 0)
            {
                throw new ArgumentException("Capacity cannot be negative");
            }

            capacity = value;
        }
    }
}
