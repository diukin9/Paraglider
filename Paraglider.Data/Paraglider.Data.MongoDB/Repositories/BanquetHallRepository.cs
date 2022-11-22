using MapsterMapper;
using Paraglider.Domain.NoSQL.Entities;
using Paraglider.Domain.NoSQL.Enums;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.Data.MongoDB.Repositories
{
    public class BanquetHallRepository 
        : NoSqlRepository<BanquetHall, CommonWeddingComponent>, 
        IBanquetHallRepository
    {
        public BanquetHallRepository(IMongoDataAccess<CommonWeddingComponent> dataAccess, IMapper mapper) 
            : base(dataAccess, mapper)
        {

        }
    }
}
