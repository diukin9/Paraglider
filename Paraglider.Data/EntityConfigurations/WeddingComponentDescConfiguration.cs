using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Paraglider.Domain.Entities;

namespace Paraglider.Data.EntityConfigurations
{
    public class WeddingComponentDescConfiguration : IEntityTypeConfiguration<WeddingComponentDesc>
    {
        public void Configure(EntityTypeBuilder<WeddingComponentDesc> builder)
        {
            builder.OwnsOne(o => o.TimeStamp, p =>
            {
                p.OwnsOne(o => o.IntervalStart, p =>
                {
                    p.Property(x => x.Hour).HasColumnName("IntervalStart_Hour");
                    p.Property(x => x.Minute).HasColumnName("IntervalStart_Minute");
                    p.Property(x => x.Second).HasColumnName("IntervalStart_Second");
                });

                p.OwnsOne(o => o.IntervalEnd, p =>
                {
                    p.Property(x => x.Hour).HasColumnName("IntervalEnd_Hour");
                    p.Property(x => x.Minute).HasColumnName("IntervalEnd_Minute");
                    p.Property(x => x.Second).HasColumnName("IntervalEnd_Second");
                });

                p.OwnsOne(o => o.ExactTime, p =>
                {
                    p.Property(x => x.Hour).HasColumnName("ExactTime_Hour");
                    p.Property(x => x.Minute).HasColumnName("ExactTime_Minute");
                    p.Property(x => x.Second).HasColumnName("ExactTime_Second");
                });

                p.Property(x => x.Type).IsRequired();
            });
        }
    }
}
