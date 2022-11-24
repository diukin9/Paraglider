using Microsoft.EntityFrameworkCore;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common.Abstractions;
using System.Linq.Expressions;

namespace Paraglider.Data.EntityFrameworkCore.Repositories;

public class UserRepository : Repository<ApplicationUser>, IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<ApplicationUser>> FindAsync(Expression<Func<ApplicationUser, bool>> selector)
    {
        return await _context.Users.IncludeAll().Where(selector).ToListAsync();
    }

    public override async Task<ApplicationUser?> FindByIdAsync(Guid id)
    {
        var user = await _context.Users.IncludeAll()
            .Where(x => x.Id == id)
            .SingleOrDefaultAsync();
        return user;
    }

    public async Task<ApplicationUser?> FindByUsernameAsync(string username)
    {
        var user = await _context.Users.IncludeAll()
            .Where(u => u.UserName == username)
            .SingleOrDefaultAsync();
        return user;
    }
}
