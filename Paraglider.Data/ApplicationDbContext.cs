using Microsoft.EntityFrameworkCore;
using Paraglider.Data.EntityConfigurations;
using Paraglider.Domain.Entities;

namespace Paraglider.Data
{
    /// <summary>
    /// Database context for current application
    /// </summary>
    public class ApplicationDbContext : DbContextBase
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<WeddingPlanning> WeddingPlannings { get; set; } = null!;
        public DbSet<WeddingComponentDesc> WeddingComponentsDesc { get; set; } = null!;

        public DbSet<BanquetHall> BanquetHalls { get; set; } = null!;
        public DbSet<Catering> Caterings { get; set; } = null!;
        public DbSet<Decorator> Decorators { get; set; } = null!;
        public DbSet<Dj> Djs { get; set; } = null!;
        public DbSet<Limousine> Limousines { get; set; } = null!;
        public DbSet<Photographer> Photographers { get; set; } = null!;
        public DbSet<PhotoStudio> PhotoStudios { get; set; } = null!;
        public DbSet<Stylist> Stylists { get; set; } = null!;
        public DbSet<Videographer> Videographers { get; set; } = null!;
        public DbSet<Confectioner> Confectioners { get; set; } = null!;
        public DbSet<Toastmaster> Toastmasters { get; set; } = null!;
        public DbSet<Registrar> Registrars { get; set; } = null!;

        public DbSet<Media> Medias { get; set; } = null!;
        public DbSet<Payment> Payments { get; set; } = null!;
        public DbSet<Premise> Premises { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;
        public DbSet<Service> Services { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Album> Albums { get; set; } = null!;
        public DbSet<City> Cities { get; set; } = null!;

        public DbSet<ExternalInfo> ExternalInfo { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ExternalInfo>().HasAlternateKey(x => new { x.ExternalProvider, x.ExternalId });

            builder.ApplyConfiguration(new PremiseConfiguration());
            builder.ApplyConfiguration(new ServiceConfiguration());
            builder.ApplyConfiguration(new WeddingComponentDescConfiguration());

            builder.ApplyConfiguration(new WeddingComponentConfiguration<BanquetHall>());
            builder.ApplyConfiguration(new WeddingComponentConfiguration<Catering>());
            builder.ApplyConfiguration(new WeddingComponentConfiguration<Decorator>());
            builder.ApplyConfiguration(new WeddingComponentConfiguration<Dj>());
            builder.ApplyConfiguration(new WeddingComponentConfiguration<Limousine>());
            builder.ApplyConfiguration(new WeddingComponentConfiguration<Photographer>());
            builder.ApplyConfiguration(new WeddingComponentConfiguration<PhotoStudio>());
            builder.ApplyConfiguration(new WeddingComponentConfiguration<Stylist>());
            builder.ApplyConfiguration(new WeddingComponentConfiguration<Videographer>());
            builder.ApplyConfiguration(new WeddingComponentConfiguration<Confectioner>());
            builder.ApplyConfiguration(new WeddingComponentConfiguration<Toastmaster>());
            builder.ApplyConfiguration(new WeddingComponentConfiguration<Registrar>());

            base.OnModelCreating(builder);
        }
    }
}