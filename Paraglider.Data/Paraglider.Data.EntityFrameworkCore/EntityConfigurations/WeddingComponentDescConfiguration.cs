using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Domain.Common.ValueObjects;

namespace Paraglider.Data.EntityFrameworkCore.EntityConfigurations;

public class WeddingComponentDescConfiguration : IEntityTypeConfiguration<WeddingComponentDesc>
{
    public void Configure(EntityTypeBuilder<WeddingComponentDesc> builder)
    {
        builder.OwnsOne(o => o.TimeInterval, p =>
        {
            p.Property(x => x.IntervalStart).HasColumnName("IntervalStart");
            p.Property(x => x.IntervalEnd).HasColumnName("IntervalEnd");
        });
    }
}
