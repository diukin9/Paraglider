using Microsoft.EntityFrameworkCore;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common.Abstractions;
using System.Linq.Expressions;

namespace Paraglider.Data.EntityFrameworkCore.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Category>> FindAsync(Expression<Func<Category, bool>> selector)
    {
        return await _context.Categories.Where(selector).ToListAsync();
    }

    public override async Task<Category?> FindByIdAsync(Guid id)
    {
        return await _context.Categories.Where(x => x.Id == id).SingleOrDefaultAsync();
    }
}
