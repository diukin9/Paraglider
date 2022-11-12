using Paraglider.AspNetCore.Identity.Domain.Abstractions;

namespace Paraglider.AspNetCore.Identity.Domain.Entities
{
    public class City : IHaveId
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Key { get; set; } = null!;
    }
}
