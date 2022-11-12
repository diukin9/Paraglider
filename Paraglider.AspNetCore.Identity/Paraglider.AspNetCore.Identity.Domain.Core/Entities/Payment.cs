using Paraglider.AspNetCore.Identity.Domain.Abstractions;
using Paraglider.AspNetCore.Identity.Domain.Enums;

namespace Paraglider.AspNetCore.Identity.Domain.Entities
{
    public class Payment : IHaveId
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public PaymentStatus Status { get; set; }
        public decimal? Sum { get; set; }
    }
}
