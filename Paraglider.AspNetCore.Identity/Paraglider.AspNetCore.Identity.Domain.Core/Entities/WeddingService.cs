using Paraglider.AspNetCore.Identity.Domain.Abstractions;
using Paraglider.AspNetCore.Identity.Domain.ValueObjects;

namespace Paraglider.AspNetCore.Identity.Domain.Entities
{
    public class WeddingService : IHaveId
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public Price Price { get; set; } = null!;
    }
}
