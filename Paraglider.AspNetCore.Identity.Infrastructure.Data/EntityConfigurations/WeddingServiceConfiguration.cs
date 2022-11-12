using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Paraglider.AspNetCore.Identity.Domain.Entities;

namespace Paraglider.AspNetCore.Identity.Domain.Data.EntityConfigurations
{
    public class WeddingServiceConfiguration : IEntityTypeConfiguration<WeddingService>
    {
        public void Configure(EntityTypeBuilder<WeddingService> builder)
        {
            builder.OwnsOne(o => o.Price, p =>
            {
                p.Property(x => x.Min).HasColumnName("MinPrice");
                p.Property(x => x.Max).HasColumnName("MaxPrice");
            });
        }
    }
}
