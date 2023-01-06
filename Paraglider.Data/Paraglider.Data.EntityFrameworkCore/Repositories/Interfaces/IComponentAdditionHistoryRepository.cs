using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common.Repository;

namespace Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;

public interface IComponentAdditionHistoryRepository : IRepository<ComponentAdditionHistory>
{
    public Task<long> CountAsync(string componentId);
}
