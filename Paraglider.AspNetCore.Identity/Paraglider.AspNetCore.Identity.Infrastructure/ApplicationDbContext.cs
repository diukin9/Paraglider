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
    }
}