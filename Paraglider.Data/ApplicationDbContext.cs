using Microsoft.EntityFrameworkCore;
using Paraglider.Data.EntityConfigurations;
using Paraglider.Domain.Entities;

namespace Paraglider.Data;

/// <summary>
/// Database context for current application
/// </summary>
public class ApplicationDbContext : DbContextBase
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<WeddingPlanning> WeddingPlannings { get; set; } = null!;
    public DbSet<WeddingComponentDesc> WeddingComponentsDesc { get; set; } = null!;

    public DbSet<BanquetHall> BanquetHalls { get; set; } = null!;
    public DbSet<Specialist> Specialists { get; set; } = null!;
    public DbSet<Limousine> Limousines { get; set; } = null!;

    public DbSet<Media> Medias { get; set; } = null!;
    public DbSet<Payment> Payments { get; set; } = null!;
    public DbSet<Hall> Halls { get; set; } = null!;
    public DbSet<Review> Reviews { get; set; } = null!;
    public DbSet<Service> Services { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Album> Albums { get; set; } = null!;
    public DbSet<City> Cities { get; set; } = null!;

    public DbSet<ExternalInfo> ExternalInfo { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ExternalInfo>().HasAlternateKey(x => new { x.ExternalProvider, x.ExternalId });

        builder.ApplyConfiguration(new HallConfiguration());
        builder.ApplyConfiguration(new ServiceConfiguration());
        builder.ApplyConfiguration(new WeddingComponentDescConfiguration());

        builder.ApplyConfiguration(new WeddingComponentConfiguration<BanquetHall>());
        builder.ApplyConfiguration(new WeddingComponentConfiguration<Specialist>());
        builder.ApplyConfiguration(new WeddingComponentConfiguration<Limousine>());

        base.OnModelCreating(builder);
    }
}