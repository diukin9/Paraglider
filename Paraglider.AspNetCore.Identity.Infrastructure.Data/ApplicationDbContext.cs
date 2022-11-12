using Microsoft.EntityFrameworkCore;
using Paraglider.AspNetCore.Identity.Domain.Data.EntityConfigurations;
using Paraglider.AspNetCore.Identity.Domain.Entities;

namespace Paraglider.AspNetCore.Identity.Domain
{
    /// <summary>
    /// Database context for current application
    /// </summary>
    public class ApplicationDbContext : DbContextBase
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<TempExternalInfo> ExternalInfo { get; set; } = null!;

        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Album> Albums { get; set; } = null!;
        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<WeddingPlan> WeddingPlans { get; set; } = null!;
        public DbSet<WPItemDesc> WPItemDescs { get; set; } = null!;
        public DbSet<Media> Medias { get; set; } = null!;
        public DbSet<Payment> Payments { get; set; } = null!;
        public DbSet<Premise> Premises { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;
        public DbSet<WeddingService> WeddingServices { get; set; } = null!;

        public DbSet<BanquetHall> BanquetHalls { get; set; } = null!;
        public DbSet<Catering> Caterings { get; set; } = null!;
        public DbSet<Decorator> Decorators { get; set; } = null!;
        public DbSet<Dj> Djs { get; set; } = null!;
        public DbSet<Limousine> Limousines { get; set; } = null!;
        public DbSet<Photographer> Photographers { get; set; } = null!;
        public DbSet<PhotoStudio> PhotoStudios { get; set; } = null!;
        public DbSet<Stylist> Stylists { get; set; } = null!;
        public DbSet<Videographer> Videographers { get; set; } = null!;
        public DbSet<WeddingCake> WeddingCakes { get; set; } = null!;
        public DbSet<WeddingHost> WeddingHosts { get; set; } = null!;
        public DbSet<WeddingRegistrar> WeddingRegistrars { get; set; } = null!;



        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TempExternalInfo>().HasAlternateKey(x => new { x.ExternalProvider, x.ExternalId });

            builder.ApplyConfiguration(new PremiseConfiguration());
            builder.ApplyConfiguration(new WeddingServiceConfiguration());
            builder.ApplyConfiguration(new WPItemDescConfiguration());

            builder.ApplyConfiguration(new WPItemConfiguration<BanquetHall>());
            builder.ApplyConfiguration(new WPItemConfiguration<Catering>());
            builder.ApplyConfiguration(new WPItemConfiguration<Decorator>());
            builder.ApplyConfiguration(new WPItemConfiguration<Dj>());
            builder.ApplyConfiguration(new WPItemConfiguration<Limousine>());
            builder.ApplyConfiguration(new WPItemConfiguration<Photographer>());
            builder.ApplyConfiguration(new WPItemConfiguration<PhotoStudio>());
            builder.ApplyConfiguration(new WPItemConfiguration<Stylist>());
            builder.ApplyConfiguration(new WPItemConfiguration<Videographer>());
            builder.ApplyConfiguration(new WPItemConfiguration<WeddingCake>());
            builder.ApplyConfiguration(new WPItemConfiguration<WeddingHost>());
            builder.ApplyConfiguration(new WPItemConfiguration<WeddingRegistrar>());

            base.OnModelCreating(builder);
        }
    }
}