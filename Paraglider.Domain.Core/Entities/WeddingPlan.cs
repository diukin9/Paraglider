using Paraglider.Domain.Abstractions;

namespace Paraglider.Domain.Entities
{
    public class WeddingPlanning : IIdentified
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CityId { get; set; }

        public virtual List<Category> SelectedCategories { get; set; } = new List<Category>();

        public virtual List<Photographer> Photographers { get; set; } = new List<Photographer>();
        public virtual List<Videographer> Videographers { get; set; } = new List<Videographer>();
        public virtual List<Toastmaster> Toastmasters { get; set; } = new List<Toastmaster>();
        public virtual List<Dj> Djs { get; set; } = new List<Dj>();
        public virtual List<Stylist> Stylists { get; set; } = new List<Stylist>();
        public virtual List<Decorator> Decorators { get; set; } = new List<Decorator>();
        public virtual List<Catering> Caterings { get; set; } = new List<Catering>();
        public virtual List<Confectioner> Confectioners { get; set; } = new List<Confectioner>();
        public virtual List<Registrar> Registrars { get; set; } = new List<Registrar>();
        public virtual List<PhotoStudio> PhotoStudios { get; set; } = new List<PhotoStudio>();
        public virtual List<Limousine> Limousines { get; set; } = new List<Limousine>();
        public virtual List<BanquetHall> BanquetHalls { get; set; } = new List<BanquetHall>();
    }
}
