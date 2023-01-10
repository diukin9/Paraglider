using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Paraglider.Domain.RDB.Entities;

namespace Paraglider.Data.EntityFrameworkCore.EntityConfigurations;

public class ComponentConfiguration : IEntityTypeConfiguration<Component>
{
    public void Configure(EntityTypeBuilder<Component> builder)
    {
        builder
            .HasOne(o => o.Album)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(o => o.Contacts)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(o => o.Reviews)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(o => o.Services)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(o => o.Halls)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class HallConfiguration : IEntityTypeConfiguration<Hall>
{
    public void Configure(EntityTypeBuilder<Hall> builder)
    {
        builder.OwnsOne(o => o.RentalPrice, p =>
        {
            p.Property(x => x.PricePerPerson).HasColumnName("PricePerPerson");
            p.Property(x => x.RentalPrice).HasColumnName("RentalPrice");
        });

        builder.OwnsOne(o => o.Capacity, p =>
        {
            p.Property(x => x.Min).HasColumnName("Min");
            p.Property(x => x.Max).HasColumnName("Max");
        });

        builder
            .HasOne(o => o.Album)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.OwnsOne(o => o.Price, p =>
        {
            p.Property(x => x.Min).HasColumnName("Min");
            p.Property(x => x.Max).HasColumnName("Max");
        });
    }
}

public class ComponentDescConfiguration : IEntityTypeConfiguration<ComponentDesc>
{
    public void Configure(EntityTypeBuilder<ComponentDesc> builder)
    {
        builder
            .OwnsOne(o => o.TimeInterval, p =>
            {
                p.Property(x => x.IntervalStart).HasColumnName("Start");
                p.Property(x => x.IntervalEnd).HasColumnName("End");
            })
            .HasMany(x => x.Payments)
            .WithOne(x => x.ComponentDesc)
            .OnDelete(DeleteBehavior.Cascade);
    }
}