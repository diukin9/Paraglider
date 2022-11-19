using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Paraglider.Domain.Entities;

namespace Paraglider.Data.EntityFrameworkCore.EntityConfigurations;

public class HallConfiguration : IEntityTypeConfiguration<Hall>
{
    public void Configure(EntityTypeBuilder<Hall> builder)
    {
        builder.OwnsOne(o => o.Capacity, p =>
        {
            p.Property(x => x.Min).HasColumnName("MinCapacity");
            p.Property(x => x.Max).HasColumnName("MaxCapacity");
        });
        builder.OwnsOne(o => o.RentalPrice, p =>
        {
            p.Property(x => x.PricePerPerson).HasColumnName("RentalPricePerPerson");
            p.Property(x => x.RentalPrice).HasColumnName("RentalPrice");
        });
    }
}
