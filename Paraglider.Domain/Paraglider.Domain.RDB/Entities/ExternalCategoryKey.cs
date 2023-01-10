using Paraglider.Domain.RDB.Enums;
using Paraglider.Infrastructure.Common.Interfaces;

namespace Paraglider.Domain.RDB.Entities;

public class ExternalCategoryKey : IIdentified
{
    public Guid Id { get; set; }
    public Source Source { get; set; }
    public string Key { get; set; } = null!;
}
