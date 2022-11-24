using MongoDB.Driver;
using Paraglider.Domain.NoSQL.Entities;
using Paraglider.Infrastructure.Common.MongoDB;

namespace Paraglider.Data.MongoDB;

public class ComponentDataAccess : MongoDataAccess<Component>
{
    private const string COLLECTION_NAME = "wedding-components";

    public ComponentDataAccess(IMongoClient client, IMongoDbSettings settings) 
        : base(client, settings, COLLECTION_NAME)
    {

    }
}
