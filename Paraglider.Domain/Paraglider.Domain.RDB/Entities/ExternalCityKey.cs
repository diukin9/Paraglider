using Paraglider.Domain.Common.Enums;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.Domain.RDB.Entities;

public class ExternalCityKey : IIdentified<Guid>
{
    public Guid Id { get; set; }
    public Source Source { get; set; }
    public string Key { get; set; } = null!;
}
