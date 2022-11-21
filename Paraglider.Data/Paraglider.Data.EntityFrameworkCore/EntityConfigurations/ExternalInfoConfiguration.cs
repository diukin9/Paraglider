using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Paraglider.Domain.RDB.Entities;

namespace Paraglider.Data.EntityFrameworkCore.EntityConfigurations;

public class ExternalInfoConfiguration : IEntityTypeConfiguration<ExternalInfo>
{
    public void Configure(EntityTypeBuilder<ExternalInfo> builder)
    {
        builder.HasAlternateKey(x => new { x.ExternalProvider, x.ExternalId });
    }
}
