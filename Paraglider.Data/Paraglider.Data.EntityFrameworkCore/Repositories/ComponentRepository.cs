using Microsoft.EntityFrameworkCore;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common.Repository;
using System.Linq.Expressions;

namespace Paraglider.Data.EntityFrameworkCore.Repositories;

public class ComponentRepository : Repository<Component>, IComponentRepository
{
    private readonly ApplicationDbContext _context;

    public ComponentRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Component>> FindAsync(
        Expression<Func<Component, bool>> selector)
    {
        var components = await _context.Components.IncludeAll()
            .Where(selector)
            .ToListAsync();

        return components;
    }

    public override async Task<Component?> FindByIdAsync(Guid id)
    {
        var component = await _context.Components.IncludeAll()
            .Where(x => x.Id == id)
            .SingleOrDefaultAsync();

        return component;
    }

    public async Task<List<Component>> FindAsync(
        Expression<Func<Component, bool>> filter,
        Expression<Func<Component, object>> orderBy,
        bool isAscending = true,
        int? skip = null,
        int? limit = null)
    {
        IQueryable<Component> query = _context.Components
            .IncludeAll()
            .Where(filter);

        query = isAscending
            ? query.OrderBy(orderBy)
            : query.OrderByDescending(orderBy);

        if (skip is not null) query = query.Skip(skip.Value);
        if (limit is not null) query = query.Take(limit.Value);

        return await query.ToListAsync();
    }
}
