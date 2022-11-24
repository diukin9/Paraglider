using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Paraglider.Domain.RDB.Entities;

namespace Paraglider.Data.EntityFrameworkCore.EntityConfigurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder
            .HasOne(x => x.Planning)
            .WithOne();

        builder
            .HasMany(x => x.Favourites)
            .WithOne(x => x.User)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(x => x.ExternalAuthInfo)
            .WithOne(x => x.User)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
