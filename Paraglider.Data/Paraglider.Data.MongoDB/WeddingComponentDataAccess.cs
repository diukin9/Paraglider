using MongoDB.Driver;
using Paraglider.Domain.NoSQL.Entities;
using Paraglider.Domain.NoSQL.Enums;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.Data.MongoDB;

public class WeddingComponentDataAccess : MongoDataAccess<CommonWeddingComponent, WeddingComponentType>
{
    private const string COLLECTION_NAME = "wedding-components";

    public WeddingComponentDataAccess(IMongoClient client, IMongoDbSettings settings) 
        : base(client, settings, COLLECTION_NAME)
    {

    }
}
