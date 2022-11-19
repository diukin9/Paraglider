using Paraglider.Domain.Entities;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;

public interface IUserRepository : IRepository<ApplicationUser>
{
    public Task<ApplicationUser?> FindByEmailAsync(string email);
    public Task<ApplicationUser?> FindByUsernameAsync(string username);
}
