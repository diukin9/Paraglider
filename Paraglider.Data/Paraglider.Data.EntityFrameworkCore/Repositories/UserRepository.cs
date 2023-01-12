using Microsoft.EntityFrameworkCore;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common.Repository;
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
        var users = await _context.Users.IncludeAll()
            .Where(selector)
            .ToListAsync();

        return users;
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
            .Where(u => u.UserName!.ToLower() == username.ToLower())
            .SingleOrDefaultAsync();

        return user;
    }

    public async Task<ApplicationUser?> FindByEmailAsync(string email)
    {
        var user = await _context.Users.IncludeAll()
            .Where(u => u.Email!.ToLower() == email.ToLower())
            .SingleOrDefaultAsync();

        return user;
    }

    public async Task<ApplicationUser?> FindByNameIdentifierAsync(string nameIdentifier)
    {
        return await FindByUsernameAsync(nameIdentifier) ?? await FindByEmailAsync(nameIdentifier);
    }
}
