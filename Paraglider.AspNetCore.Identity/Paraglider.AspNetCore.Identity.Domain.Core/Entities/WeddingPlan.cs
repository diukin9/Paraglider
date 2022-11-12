using Paraglider.AspNetCore.Identity.Domain.Abstractions;

namespace Paraglider.AspNetCore.Identity.Domain.Entities
{
    public class WeddingPlan : IHaveId
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CityId { get; set; }

        public virtual List<Category> UsedCategories { get; set; } = new List<Category>();

        public virtual List<Photographer> Photographers { get; set; } = new List<Photographer>();
        public virtual List<Videographer> Videographers { get; set; } = new List<Videographer>();
        public virtual List<WeddingHost> WeddingHosts { get; set; } = new List<WeddingHost>();
        public virtual List<Dj> Djs { get; set; } = new List<Dj>();
        public virtual List<Stylist> Stylists { get; set; } = new List<Stylist>();
        public virtual List<Decorator> Decorators { get; set; } = new List<Decorator>();
        public virtual List<Catering> Caterings { get; set; } = new List<Catering>();
        public virtual List<WeddingCake> WeddingCakes { get; set; } = new List<WeddingCake>();
        public virtual List<WeddingRegistrar> Registrars { get; set; } = new List<WeddingRegistrar>();
        public virtual List<PhotoStudio> PhotoStudios { get; set; } = new List<PhotoStudio>();
        public virtual List<Limousine> Limousines { get; set; } = new List<Limousine>();
        public virtual List<BanquetHall> BanquetHalls { get; set; } = new List<BanquetHall>();
    }
}
