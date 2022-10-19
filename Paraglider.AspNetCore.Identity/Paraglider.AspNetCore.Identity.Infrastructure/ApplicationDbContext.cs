using Microsoft.EntityFrameworkCore;
using Paraglider.AspNetCore.Identity.Infrastructure.Base;

namespace Paraglider.AspNetCore.Identity.Infrastructure
{
    /// <summary>
    /// Database context for current application
    /// </summary>
    public class ApplicationDbContext : DbContextBase
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ExternalInfo> ExternalInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ExternalInfo>().HasAlternateKey(x => new { x.ExternalProvider, x.ExternalId });
            base.OnModelCreating(builder);
        }
    }
}