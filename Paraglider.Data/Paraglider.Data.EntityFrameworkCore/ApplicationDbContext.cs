using Microsoft.EntityFrameworkCore;
using Paraglider.Data.EntityFrameworkCore.EntityConfigurations;
using Paraglider.Domain.RDB.Entities;

namespace Paraglider.Data;

public class ApplicationDbContext : DbContextBase
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<ExternalAuthInfo> ExternalAuthInfo { get; set; } = null!;
    public DbSet<City> Cities { get; set; } = null!;
    public DbSet<UserComponent> UserComponents { get; set; } //

    public DbSet<Planning> Plannings { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<PlanningComponent> PlanningComponents { get; set; } = null!; //

    public DbSet<ComponentDesc> ComponentDescs { get; set; } = null!; //
    public DbSet<Payment> Payments { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new ApplicationUserConfiguration());
        builder.ApplyConfiguration(new ExternalAuthInfoConfiguration());

        builder.ApplyConfiguration(new PlanningConfiguration());
        builder.ApplyConfiguration(new ComponentDescConfiguration());
        builder.ApplyConfiguration(new PlanningComponentConfiguration());

        base.OnModelCreating(builder);
    }
}