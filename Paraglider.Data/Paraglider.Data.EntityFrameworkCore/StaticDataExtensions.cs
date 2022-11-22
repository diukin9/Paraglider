using Microsoft.EntityFrameworkCore;
using Paraglider.Domain.RDB.Entities;

namespace Paraglider.Data.EntityFrameworkCore
{
    public static class StaticDataExtensions
    {
        public static IQueryable<ApplicationUser> IncludeAll(this IQueryable<ApplicationUser> queryable)
        {
            return queryable
                .Include(x => x.City)
                .Include(x => x.WeddingPlannings)
                .Include(x => x.ExternalAuthInfo);
        }
    }
}
