using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Paraglider.Domain.RDB.Entities;

namespace Paraglider.Data.EntityFrameworkCore.EntityConfigurations;

public class ExternalAuthInfoConfiguration : IEntityTypeConfiguration<ExtlAuthInfo>
{
    public void Configure(EntityTypeBuilder<ExtlAuthInfo> builder)
    {
        builder.HasAlternateKey(x => new { x.Provider, x.ExternalId });
    }
}
