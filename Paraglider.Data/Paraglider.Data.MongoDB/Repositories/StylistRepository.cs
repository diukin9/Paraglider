using MapsterMapper;
using Paraglider.Domain.NoSQL.Entities;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.Data.MongoDB.Repositories
{
    public class StylistRepository 
        : NoSqlRepository<Stylist, CommonWeddingComponent>,
        IStylistRepository
    {
        public StylistRepository(IMongoDataAccess<CommonWeddingComponent> dataAccess, IMapper mapper) 
            : base(dataAccess, mapper)
        {

        }
    }
}
