using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Paraglider.Domain.Entities;

namespace Paraglider.Data.EntityFrameworkCore.EntityConfigurations;

public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.OwnsOne(o => o.Price, p =>
        {
            p.Property(x => x.Min).HasColumnName("MinPrice");
            p.Property(x => x.Max).HasColumnName("MaxPrice");
        });
    }
}
