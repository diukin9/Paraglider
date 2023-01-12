using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Paraglider.Domain.RDB.Entities;

namespace Paraglider.Data.EntityFrameworkCore.EntityConfigurations;

public class ComponentConfiguration : IEntityTypeConfiguration<Component>
{
    public void Configure(EntityTypeBuilder<Component> builder)
    {
        builder
            .HasOne(x => x.Album)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(o => o.Contacts)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(o => o.Reviews)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(o => o.Services)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(o => o.Halls)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class HallConfiguration : IEntityTypeConfiguration<Hall>
{
    public void Configure(EntityTypeBuilder<Hall> builder)
    {
        builder.OwnsOne(o => o.Price, p =>
        {
            p.Property(x => x.PricePerPerson);
            p.Property(x => x.RentalPrice);
        });

        builder.OwnsOne(o => o.Capacity, p =>
        {
            p.Property(x => x.Min);
            p.Property(x => x.Max);
        });

        builder
            .HasOne(o => o.Album)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.OwnsOne(o => o.Price, p =>
        {
            p.Property(x => x.Min);
            p.Property(x => x.Max);
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
                p.Property(x => x.IntervalStart);
                p.Property(x => x.IntervalEnd);
            })
            .HasMany(x => x.Payments)
            .WithOne(x => x.ComponentDesc)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class ComponentAddHistoryConfiguration : IEntityTypeConfiguration<ComponentAddHistory>
{
    public void Configure(EntityTypeBuilder<ComponentAddHistory> builder)
    {
        builder
            .HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId);

        builder
            .HasOne(x => x.Component)
            .WithMany()
            .HasForeignKey(x => x.ComponentId);
    }
}

public class AlbumConfiguration : IEntityTypeConfiguration<Album>
{
    public void Configure(EntityTypeBuilder<Album> builder)
    {
        builder
            .HasMany(o => o.Media)
            .WithOne()
            .HasForeignKey(o => o.AlbumId);
    }
}