using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Paraglider.AspNetCore.Identity.Domain.Entities;

namespace Paraglider.AspNetCore.Identity.Domain.Data.EntityConfigurations
{
    public class WPItemDescConfiguration : IEntityTypeConfiguration<WPItemDesc>
    {
        public void Configure(EntityTypeBuilder<WPItemDesc> builder)
        {
            builder.OwnsOne(o => o.TimeData, p =>
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
