using Paraglider.Domain.Abstractions;

namespace Paraglider.Domain.Entities;

public class WeddingPlanning : IIdentified
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid CityId { get; set; }
    public virtual City City { get; set; } = null!;  

    public virtual List<Category> SelectedCategories { get; set; } = new List<Category>();

    public virtual List<Specialist> Specialists { get; set; } = new List<Specialist>();
    public virtual List<Limousine> Limousines { get; set; } = new List<Limousine>();
    public virtual List<BanquetHall> BanquetHalls { get; set; } = new List<BanquetHall>();
}
