using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Paraglider.Domain.Entities;

namespace Paraglider.Data.EntityConfigurations;

public class WeddingComponentDescConfiguration : IEntityTypeConfiguration<WeddingComponentDesc>
{
    public void Configure(EntityTypeBuilder<WeddingComponentDesc> builder)
    {
        builder.OwnsOne(o => o.TimeStamp, p =>
        {
            p.Property(x => x.IntervalStart).HasColumnName("IntervalStart");
            p.Property(x => x.IntervalEnd).HasColumnName("IntervalEnd");
        });
    }
}
