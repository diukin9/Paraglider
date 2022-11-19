using Paraglider.Domain.NoSQL.Enums;

namespace Paraglider.Domain.NoSQL.ValueObjects;

public class Media
{
    public string Id { get; set; } = null!;
    public MediaType Type { get; set; }
    public string Url { get; set; } = null!;
}