namespace Paraglider.Domain.NoSQL.ValueObjects;

public class Album
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public virtual List<Media> Media { get; set; } = new List<Media>();
}