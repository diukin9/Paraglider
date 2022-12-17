using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common.Repository;

namespace Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;

public interface IUserRepository : IRepository<ApplicationUser>
{
    public Task<ApplicationUser?> FindByUsernameAsync(string username);
    public Task<bool> ChangeCity(Guid userId, Guid newCityId, CancellationToken cancellationToken);
}
