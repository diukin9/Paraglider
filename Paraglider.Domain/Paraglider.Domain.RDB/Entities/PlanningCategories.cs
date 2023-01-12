namespace Paraglider.Domain.RDB.Entities;

public class PlanningCategories
{
    public Guid PlanningId { get; set; }
    public virtual Planning Planning { get; set; } = null!;

    public Guid CategoryId { get; set; }
    public virtual Category Category { get; set; } = null!;
}
