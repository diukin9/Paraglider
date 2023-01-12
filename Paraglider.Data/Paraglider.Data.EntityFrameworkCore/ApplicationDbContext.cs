using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Paraglider.Data.EntityFrameworkCore.EntityConfigurations;
using Paraglider.Domain.RDB.Entities;

namespace Paraglider.Data.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<ExtlAuthInfo> ExternalAuthInfo { get; set; } = null!;

    public DbSet<City> Cities { get; set; } = null!;

    public DbSet<Planning> Plannings { get; set; } = null!;

    public DbSet<Category> Categories { get; set; } = null!;

    public DbSet<PlanningComponent> PlanningComponents { get; set; } = null!;

    public DbSet<ComponentDesc> ComponentDescs { get; set; } = null!; 

    public DbSet<ComponentAddHistory> ComponentAdditionHistory { get; set; } = null!;

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
        builder.ApplyConfiguration(new CategoryConfiguration());
        builder.ApplyConfiguration(new CityConfiguration());

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