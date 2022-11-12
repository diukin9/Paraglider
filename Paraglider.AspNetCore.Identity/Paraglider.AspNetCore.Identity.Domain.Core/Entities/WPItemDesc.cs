using Paraglider.AspNetCore.Identity.Domain.Abstractions;
using Paraglider.AspNetCore.Identity.Domain.Enums;
using Paraglider.AspNetCore.Identity.Domain.ValueObjects;

namespace Paraglider.AspNetCore.Identity.Domain.Entities
{
    public class WPItemDesc : IHaveId
    {
        public Guid Id { get; set; }
        public Guid ConfigurationItemId { get; set; }
        public WPItemStatus Status { get; set; }
        public Time TimeData { get; set; } = null!;
        public virtual List<Payment> Payments { get; set; } = new List<Payment>();
    }
}
