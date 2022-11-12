using Paraglider.Domain.Abstractions;

namespace Paraglider.Domain.Entities
{
    public class Limousine : WeddingComponent
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
                if (value.HasValue && value.Value > 50)
                {
                    throw new ArgumentException("Such large limousines do not exist");
                }

                capacity = value;
            }
        }
    }
}
