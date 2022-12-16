using Paraglider.Domain.Common.Enums;

namespace Paraglider.Domain.RDB.Entities;

public class ExternalCategoryKey
{
    public Guid Id { get; set; }
    public Source Source { get; set; }
    public string Key { get; set; } = null!;
}
