using Paraglider.Domain.Abstractions;
using Paraglider.Domain.ValueObjects;

namespace Paraglider.Domain.Entities
{
    public class Limousine : IHaveId, IWPItem
    {
        public Guid Id { get; set; }
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

        public string Name { get; set; } = null!;
        public string? Description { get; set; } = null!;
        public ExternalInfo ExternalInfo { get; set; } = null!;
        public Contacts Contacts { get; set; } = null!;

        public Guid CityId { get; set; }
        public virtual City City { get; set; } = null!;

        public Guid AlbumId { get; set; }
        public virtual Album Album { get; set; } = null!;

        public virtual List<Review> Reviews { get; set; } = new List<Review>();
    }
}
