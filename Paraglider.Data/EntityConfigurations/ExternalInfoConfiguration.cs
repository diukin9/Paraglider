using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Paraglider.Domain.Entities;

namespace Paraglider.Data.EntityConfigurations;

public class ExternalInfoConfiguration : IEntityTypeConfiguration<ExternalInfo>
{
    public void Configure(EntityTypeBuilder<ExternalInfo> builder)
    {
        builder.HasAlternateKey(x => new { x.ExternalProvider, x.ExternalId });
    }
}
