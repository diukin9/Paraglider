using MapsterMapper;
using Paraglider.Data.MongoDB.Repositories.Interfaces;
using Paraglider.Domain.NoSQL.Entities;
using Paraglider.Domain.NoSQL.Enums;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.Data.MongoDB.Repositories
{
    public class PhotoStudioRepository 
        : NoSqlRepository<PhotoStudio, CommonWeddingComponent>, 
        IPhotoStudioRepository
    {
        public PhotoStudioRepository(IMongoDataAccess<CommonWeddingComponent> dataAccess, IMapper mapper) 
            : base(dataAccess, mapper)
        {

        }
    }
}
