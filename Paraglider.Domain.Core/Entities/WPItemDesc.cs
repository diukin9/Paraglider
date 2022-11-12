using Paraglider.Domain.Abstractions;
using Paraglider.Domain.Enums;
using Paraglider.Domain.ValueObjects;

namespace Paraglider.Domain.Entities
{
    public class WPItemDesc : IHaveId
    {
        public Guid Id { get; set; }
        public Guid ConfigurationItemId { get; set; }
        public WPItemStatus Status { get; set; }
        public TimeStamp TimeStamp { get; set; } = null!;
        public virtual List<Payment> Payments { get; set; } = new List<Payment>();
    }
}
