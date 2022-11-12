using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Paraglider.AspNetCore.Identity.Domain.Abstractions;
using Paraglider.AspNetCore.Identity.Domain.Entities;

namespace Paraglider.AspNetCore.Identity.Domain
{
    /// <summary>
    /// Base DbContext with predefined configuration
    /// </summary>
    public abstract class DbContextBase : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        private const string DefaultUserName = "Anonymous";

        protected DbContextBase(DbContextOptions options) : base(options) { }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                DbSaveChanges();
                return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            }
            catch 
            { 
                return Task.FromResult(0); 
            }
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            try
            {
                DbSaveChanges();
                return base.SaveChanges(acceptAllChangesOnSuccess);
            }
            catch 
            { 
                return 0; 
            }
        }

        public override int SaveChanges()
        {
            try
            {
                DbSaveChanges();
                return base.SaveChanges();
            }
            catch 
            { 
                return 0; 
            }
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                DbSaveChanges();
                return base.SaveChangesAsync(cancellationToken);
            }
            catch 
            { 
                return Task.FromResult(0); 
            }
        }

        private void DbSaveChanges()
        {
            var createdEntries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added && e.Entity is IAuditable)
                .ToList();

            foreach (var entry in createdEntries)
            {
                entry.Property("UpdatedAt").CurrentValue
                    = entry.Property("CreatedAt").CurrentValue
                    = DateTime.Now.ToUniversalTime();

                entry.Property("UpdatedBy").CurrentValue
                    = entry.Property("CreatedBy").CurrentValue
                    = entry.Property("CreatedBy").CurrentValue ?? DefaultUserName;
            }

            var updatedEntries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified && e.Entity is IAuditable)
                .ToList();

            foreach (var entry in updatedEntries)
            {
                entry.Property("UpdatedAt").CurrentValue = DateTime.Now.ToUniversalTime();
                entry.Property("UpdatedBy").CurrentValue = entry.Property("UpdatedBy").CurrentValue ?? DefaultUserName;
            }
        }
    }
}