using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common.Repository;

namespace Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;

public interface IUserRepository : IRepository<ApplicationUser>
{
    public Task<ApplicationUser?> FindByUsernameAsync(string username);
    public Task<ApplicationUser?> FindByEmailAsync(string email);
    public Task<ApplicationUser?> FindByNameIdentifierAsync(string nameIdentifier);
}
