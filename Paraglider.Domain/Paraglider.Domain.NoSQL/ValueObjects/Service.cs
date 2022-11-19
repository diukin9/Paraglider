namespace Paraglider.Domain.NoSQL.ValueObjects;

public class Service
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public Price Price { get; set; } = null!;
}

