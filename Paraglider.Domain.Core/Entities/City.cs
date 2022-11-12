using Paraglider.Domain.Abstractions;

namespace Paraglider.Domain.Entities
{
    public class City : IIdentified
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Key { get; set; } = null!;
    }
}
