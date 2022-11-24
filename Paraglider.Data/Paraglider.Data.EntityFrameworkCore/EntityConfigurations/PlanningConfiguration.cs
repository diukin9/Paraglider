using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Paraglider.Domain.RDB.Entities;

namespace Paraglider.Data.EntityFrameworkCore.EntityConfigurations;

public class PlanningConfiguration : IEntityTypeConfiguration<Planning>
{
    public void Configure(EntityTypeBuilder<Planning> builder)
    {
        builder
            .HasMany(x => x.Categories)
            .WithMany()
            .UsingEntity(x => x.ToTable("PlanningCategories"));

        builder
            .HasMany(x => x.PlanningComponents)
            .WithOne(x => x.Planning);
    }
}
