using Paraglider.Infrastructure.Common.Interfaces;

namespace Paraglider.Domain.RDB.Entities
{
    public class ComponentAdditionHistory : IAggregateRoot
    {
        public Guid Id { get; set; }

        public string ComponentId { get; set; } = null!;

        public Guid UserId { get; set; }
    }
}
