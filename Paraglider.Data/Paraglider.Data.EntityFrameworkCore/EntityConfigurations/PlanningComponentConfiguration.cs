using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Paraglider.Domain.RDB.Entities;

namespace Paraglider.Data.EntityFrameworkCore.EntityConfigurations;

public class PlanningComponentConfiguration : IEntityTypeConfiguration<PlanningComponent>
{
    public void Configure(EntityTypeBuilder<PlanningComponent> builder)
    {
        builder
            .HasOne(x => x.ComponentDesc)
            .WithOne(x => x.PlanningComponent)
            .HasForeignKey<ComponentDesc>()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
