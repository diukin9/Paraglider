using Microsoft.EntityFrameworkCore;
using Paraglider.Data.EntityFrameworkCore.EntityConfigurations;
using Paraglider.Domain.NoSQL.ValueObjects;
using Paraglider.Domain.RDB.Entities;

namespace Paraglider.Data;

/// <summary>
/// Database context for current application
/// </summary>
public class ApplicationDbContext : DbContextBase
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<WeddingPlanning> WeddingPlannings { get; set; } = null!;
    public DbSet<WeddingComponentDesc> WeddingComponentsDesc { get; set; } = null!;

    public DbSet<Payment> Payments { get; set; } = null!;
    public DbSet<City> Cities { get; set; } = null!;
    public DbSet<ExternalInfo> ExternalInfo { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new ExternalInfoConfiguration());
        builder.ApplyConfiguration(new WeddingComponentDescConfiguration());

        base.OnModelCreating(builder);
    }
}