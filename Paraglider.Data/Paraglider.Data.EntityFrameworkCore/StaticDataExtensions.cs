using Microsoft.EntityFrameworkCore;
using Paraglider.Domain.RDB.Entities;

namespace Paraglider.Data.EntityFrameworkCore;

public static class StaticDataExtensions
{
    public static IQueryable<ApplicationUser> IncludeAll(this IQueryable<ApplicationUser> queryable)
    {
        return queryable
            .Include(x => x.City)
            .Include(x => x.ExternalAuthInfo)
            .Include(x => x.Favourites)
            .ThenInclude(x => x.Album)
            .ThenInclude(x => x!.Media)
            .Include(x => x.Favourites)
            .ThenInclude(x => x.Services)
            .Include(x => x.Favourites)
            .ThenInclude(x => x.Category)
            .ThenInclude(x => x.Keys)
            .Include(x => x.Favourites)
            .ThenInclude(x => x.City)
            .ThenInclude(x => x.Keys)
            .Include(x => x.Favourites)
            .ThenInclude(x => x.Contacts)
            .Include(x => x.Favourites)
            .ThenInclude(x => x.Halls)!
            .ThenInclude(x => x.Album)
            .ThenInclude(x => x!.Media)
            .Include(x => x.Favourites)
            .ThenInclude(x => x.Reviews)
            .Include(x => x.Planning)
            .ThenInclude(x => x.PlanningComponents)
            .ThenInclude(x => x.Component)
            .ThenInclude(x => x.Album)
            .ThenInclude(x => x!.Media)
            .Include(x => x.Planning)
            .ThenInclude(x => x.PlanningComponents)
            .ThenInclude(x => x.Component)
            .ThenInclude(x => x.Services)
            .Include(x => x.Planning)
            .ThenInclude(x => x.PlanningComponents)
            .ThenInclude(x => x.Component)
            .ThenInclude(x => x.Category)
            .ThenInclude(x => x.Keys)
            .Include(x => x.Planning)
            .ThenInclude(x => x.PlanningComponents)
            .ThenInclude(x => x.Component)
            .ThenInclude(x => x.City)
            .ThenInclude(x => x.Keys)
            .Include(x => x.Planning)
            .ThenInclude(x => x.PlanningComponents)
            .ThenInclude(x => x.Component)
            .ThenInclude(x => x.Contacts)
            .Include(x => x.Planning)
            .ThenInclude(x => x.PlanningComponents)
            .ThenInclude(x => x.Component)
            .ThenInclude(x => x.Halls)!
            .ThenInclude(x => x.Album)
            .ThenInclude(x => x!.Media)
            .Include(x => x.Planning)
            .ThenInclude(x => x.PlanningComponents)
            .ThenInclude(x => x.Component)
            .ThenInclude(x => x.Reviews)
            .Include(x => x.Planning)
            .ThenInclude(x => x.PlanningComponents)
            .ThenInclude(x => x.ComponentDesc)
            .ThenInclude(x => x.Payments)
            .Include(x => x.Planning)
            .ThenInclude(x => x.PlanningComponents)
            .ThenInclude(x => x.Category)
            .Include(x => x.Planning)
            .ThenInclude(x => x.Categories);
    }

    public static IQueryable<City> IncludeAll(this IQueryable<City> queryable)
    {
        return queryable.Include(x => x.Keys);
    }

    public static IQueryable<Category> IncludeAll(this IQueryable<Category> queryable)
    {
        return queryable.Include(x => x.Keys);
    }

    public static IQueryable<Component> IncludeAll(this IQueryable<Component> quearyable)
    {
        return quearyable
            .Include(x => x.Album)
            .ThenInclude(x => x!.Media)
            .Include(x => x.Services)
            .Include(x => x.Category)
            .ThenInclude(x => x.Keys)
            .Include(x => x.City)
            .ThenInclude(x => x.Keys)
            .Include(x => x.Contacts)
            .Include(x => x.Halls)!
            .ThenInclude(x => x.Album)
            .ThenInclude(x => x!.Media)
            .Include(x => x.Reviews);
    }

    public static IQueryable<PlanningComponent> IncludeAll(this IQueryable<PlanningComponent> queryable)
    {
        return queryable
            .Include(x => x.Category)
            .Include(x => x.ComponentDesc)
            .ThenInclude(x => x.Payments)
            .Include(x => x.Component)
            .ThenInclude(x => x.Album)
            .ThenInclude(x => x!.Media)
            .Include(x => x.Component)
            .ThenInclude(x => x.Services)
            .Include(x => x.Component)
            .ThenInclude(x => x.Category)
            .ThenInclude(x => x.Keys)
            .Include(x => x.Component)
            .ThenInclude(x => x.City)
            .ThenInclude(x => x.Keys)
            .Include(x => x.Component)
            .ThenInclude(x => x.Contacts)
            .Include(x => x.Component)
            .ThenInclude(x => x.Halls)!
            .ThenInclude(x => x.Album)
            .ThenInclude(x => x!.Media)
            .Include(x => x.Component)
            .ThenInclude(x => x.Reviews);
    }
}
