using Paraglider.Domain.Abstractions;

namespace Paraglider.Domain.Entities
{
    public class Category : IIdentified
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
