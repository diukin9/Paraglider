using Microsoft.EntityFrameworkCore;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common.Repository;
using System.Linq.Expressions;

namespace Paraglider.Data.EntityFrameworkCore.Repositories;

public class ComponentAdditionHistoryRepository
    : Repository<ComponentAdditionHistory>, IComponentAdditionHistoryRepository
{
    private readonly ApplicationDbContext _context;

    public ComponentAdditionHistoryRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<long> CountAsync(string componentId)
    {
        return await _context.ComponentAdditionHistory
            .Where(x => x.ComponentId == componentId)
            .CountAsync();
    }

    public override async Task<IEnumerable<ComponentAdditionHistory>> FindAsync(
        Expression<Func<ComponentAdditionHistory, bool>> selector)
    {
        return await _context.ComponentAdditionHistory
            .Where(selector)
            .ToListAsync();
    }

    public override async Task<ComponentAdditionHistory?> FindByIdAsync(Guid id)
    {
        return await _context.ComponentAdditionHistory
            .Where(x => x.Id == id)
            .SingleOrDefaultAsync();
    }
}
