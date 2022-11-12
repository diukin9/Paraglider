using Paraglider.AspNetCore.Identity.Domain.Abstractions;
using Paraglider.AspNetCore.Identity.Domain.ValueObjects;

namespace Paraglider.AspNetCore.Identity.Domain.Entities
{
    public class Premise : IHaveId
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public PremiseRentalPrice RentalPrice { get; set; } = null!;
        public Capacity Capacity { get; set; } = null!;

        public Guid AlbumId { get; set; }
        public virtual Album Album { get; set; } = null!;

        private decimal? minimalPrice;
        public decimal? MinimalPrice 
        { 
            get => minimalPrice; 
            set
            {
                if (value.HasValue && value.Value < 0)
                {
                    throw new ArgumentException("Price cannot be negative");
                }
                minimalPrice = value;
            }
        }
    }
}
