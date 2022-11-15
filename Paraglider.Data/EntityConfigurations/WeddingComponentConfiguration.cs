using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Paraglider.Domain.Abstractions;

namespace Paraglider.Data.EntityConfigurations;

internal class WeddingComponentConfiguration<T> : IEntityTypeConfiguration<T> where T : class, IWeddingComponent
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.OwnsOne(o => o.Contacts, p =>
        {
            p.Property(x => x.PhoneNumber).HasColumnName("PhoneNumber").IsRequired();
        });
    }
}
