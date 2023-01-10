using Microsoft.EntityFrameworkCore;
using Paraglider.Data.EntityFrameworkCore.EntityConfigurations;
using Paraglider.Domain.RDB.Entities;

namespace Paraglider.Data.EntityFrameworkCore;

public class ApplicationDbContext : DbContextBase
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<ExternalAuthInfo> ExternalAuthInfo { get; set; } = null!;

    public DbSet<City> Cities { get; set; } = null!;

    public DbSet<UserComponent> UserComponents { get; set; }

    public DbSet<Planning> Plannings { get; set; } = null!;

    public DbSet<Category> Categories { get; set; } = null!;

    public DbSet<PlanningComponent> PlanningComponents { get; set; } = null!;

    public DbSet<ComponentDesc> ComponentDescs { get; set; } = null!; 

    public DbSet<ComponentAdditionHistory> ComponentAdditionHistory { get; set; } = null!;

    public DbSet<Payment> Payments { get; set; } = null!;

    public DbSet<Component> Components { get; set; } = null!;

    public DbSet<Album> Albums { get; set; }

    public DbSet<Media> Media { get; set; }

    public DbSet<Contact> Contacts { get; set; } = null!;

    public DbSet<Review> Reviews { get; set; } = null!;

    public DbSet<Service> Services { get; set; } = null!;

    public DbSet<Hall> Halls { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new ApplicationUserConfiguration());
        builder.ApplyConfiguration(new ExternalAuthInfoConfiguration());

        builder.ApplyConfiguration(new ComponentConfiguration());
        builder.ApplyConfiguration(new HallConfiguration());
        builder.ApplyConfiguration(new ServiceConfiguration());
        builder.ApplyConfiguration(new ComponentDescConfiguration());
        builder.ApplyConfiguration(new AlbumConfiguration());

        builder.ApplyConfiguration(new PlanningConfiguration());
        builder.ApplyConfiguration(new PlanningComponentConfiguration());

        base.OnModelCreating(builder);
    }
}