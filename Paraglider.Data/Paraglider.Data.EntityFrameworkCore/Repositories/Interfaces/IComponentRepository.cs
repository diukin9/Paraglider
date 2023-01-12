using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common.Repository;
using System.Linq.Expressions;

namespace Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;

public interface IComponentRepository : IRepository<Component>
{
    public Task<List<Component>> FindAsync(
        Expression<Func<Component, bool>> filter,
        Expression<Func<Component, object>> orderBy,
        bool isAscending = true,
        int? skip = null,
        int? limit = null);
}
