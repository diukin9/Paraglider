using Paraglider.Domain.Abstractions;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.Domain.Entities;

public class City : IIdentified, IAggregateRoot
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Key { get; set; } = null!;
}
