using MapsterMapper;
using Paraglider.Domain.NoSQL.Entities;
using Paraglider.Domain.NoSQL.Enums;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.Data.MongoDB.Repositories
{
    public class CateringRepository 
        : NoSqlRepository<Catering, CommonWeddingComponent>, 
        ICateringRepository
    {
        public CateringRepository(IMongoDataAccess<CommonWeddingComponent> dataAccess, IMapper mapper) 
            : base(dataAccess, mapper)
        {

        }
    }
}
