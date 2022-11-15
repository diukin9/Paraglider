using Paraglider.Domain.Entities;
using Paraglider.Infrastructure;

namespace Paraglider.Data.Repositories.Interfaces;

public interface IUserRepository : IRepository<ApplicationUser>
{
    public Task<ApplicationUser?> FindByEmailAsync(string email);
    public Task<ApplicationUser?> FindByUsernameAsync(string username);
}
