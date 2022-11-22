using MapsterMapper;
using Paraglider.Domain.NoSQL.Entities;
using Paraglider.Domain.NoSQL.Enums;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.Data.MongoDB.Repositories
{
    public class DecoratorRepository 
        : NoSqlRepository<Decorator, CommonWeddingComponent>, 
        IDecoratorRepository
    {
        public DecoratorRepository(IMongoDataAccess<CommonWeddingComponent> dataAccess, IMapper mapper) 
            : base(dataAccess, mapper)
        {

        }
    }
}
