using Microsoft.EntityFrameworkCore;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common.Repository;
using System.Linq.Expressions;

namespace Paraglider.Data.EntityFrameworkCore.Repositories;

public class PlanningComponentRepository : Repository<PlanningComponent>, IPlanningComponentRepository
{
    private readonly ApplicationDbContext _context;

    public PlanningComponentRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<PlanningComponent>> FindAsync(
        Expression<Func<PlanningComponent, bool>> selector)
    {
        var plannings = await _context.PlanningComponents.IncludeAll()
            .Where(selector)
            .ToListAsync();

        return plannings;
    }

    public override async Task<PlanningComponent?> FindByIdAsync(Guid id)
    {
        var planning = await _context.PlanningComponents.IncludeAll()
            .Where(x => x.Id == id)
            .SingleOrDefaultAsync();

        return planning;
    }
}
