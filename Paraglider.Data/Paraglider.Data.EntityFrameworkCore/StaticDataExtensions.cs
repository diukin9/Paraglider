using Microsoft.EntityFrameworkCore;
using Paraglider.Domain.RDB.Entities;

namespace Paraglider.Data.EntityFrameworkCore;

public static class StaticDataExtensions
{
    public static IQueryable<ApplicationUser> IncludeAll(this IQueryable<ApplicationUser> queryable)
    {
        return queryable
            .Include(x => x.City)
            .Include(x => x.Favourites)
            .Include(x => x.ExternalAuthInfo)
            .Include(x => x.Planning)
                .ThenInclude(x => x.Categories)
            .Include(x => x.Planning)
                .ThenInclude(x => x.PlanningComponents)
                    .ThenInclude(x => x.ComponentDesc)
                        .ThenInclude(x => x.Payments)
            .Include(x => x.Planning)
                .ThenInclude(x => x.PlanningComponents)
                    .ThenInclude(x => x.Category);
    }
}
