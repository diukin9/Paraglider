using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Paraglider.Domain.RDB.Entities;

namespace Paraglider.Data.EntityFrameworkCore.EntityConfigurations;

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
