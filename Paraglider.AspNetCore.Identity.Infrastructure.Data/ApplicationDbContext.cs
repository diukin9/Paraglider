using Microsoft.EntityFrameworkCore;
using Paraglider.AspNetCore.Identity.Infrastructure.Data.Base;

namespace Paraglider.AspNetCore.Identity.Infrastructure.Data
{
    /// <summary>
    /// Database context for current application
    /// </summary>
    public class ApplicationDbContext : DbContextBase
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ExternalInfo> ExternalInfo { get; set; }
    }
}