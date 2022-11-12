using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Paraglider.Domain.Abstractions;

namespace Paraglider.Data.EntityConfigurations
{
    internal class WPItemConfiguration<T> : IEntityTypeConfiguration<T> where T : class, IWPItem
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.OwnsOne(o => o.ExternalInfo, p =>
            {
                p.Property(x => x.Id).HasColumnName("ExternalId").IsRequired();
                p.Property(x => x.Provider).HasColumnName("Provider").IsRequired();
            });
            builder.OwnsOne(o => o.Contacts, p =>
            {
                p.Property(x => x.PhoneNumber).HasColumnName("PhoneNumber").IsRequired();
            });
        }
    }
}
