using MongoDB.Driver;
using Paraglider.Domain.NoSQL.Entities;
using Paraglider.Infrastructure.Common.MongoDB;

namespace Paraglider.Data.MongoDB;

public class ComponentDataAccess : MongoDataAccess<Component>
{
    public ComponentDataAccess(IMongoClient client, IMongoDbSettings settings) : base(client, settings)
    {

    }
}
