using Microsoft.EntityFrameworkCore;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common.Repository;
using System.Linq.Expressions;

namespace Paraglider.Data.EntityFrameworkCore.Repositories;

public class ComponentAddHistoryRepository
    : Repository<ComponentAddHistory>, IComponentAddHistoryRepository
{
    private readonly ApplicationDbContext _context;

    public ComponentAddHistoryRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<ComponentAddHistory>> FindAsync(
        Expression<Func<ComponentAddHistory, bool>> selector)
    {
        var history = await _context.ComponentAdditionHistory
            .Where(selector)
            .ToListAsync();

        return history;
    }

    public override async Task<ComponentAddHistory?> FindByIdAsync(Guid id)
    {
        var history = await _context.ComponentAdditionHistory
            .Where(x => x.Id == id)
            .SingleOrDefaultAsync();

        return history;
    }
}
