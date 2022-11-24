using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Paraglider.Domain.RDB.Entities;

namespace Paraglider.Data.EntityFrameworkCore.EntityConfigurations;

public class ExternalAuthInfoConfiguration : IEntityTypeConfiguration<ExternalAuthInfo>
{
    public void Configure(EntityTypeBuilder<ExternalAuthInfo> builder)
    {
        builder.HasAlternateKey(x => new { x.ExternalProvider, x.ExternalId });
    }
}
