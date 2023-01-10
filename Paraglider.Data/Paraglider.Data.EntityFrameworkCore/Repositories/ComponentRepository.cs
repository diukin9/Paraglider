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

    public override Task<IEnumerable<Component>> FindAsync(Expression<Func<Component, bool>> selector)
    {
        throw new NotImplementedException();
    }

    public override Task<Component?> FindByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
