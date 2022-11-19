using MongoDB.Driver;
using Paraglider.Domain.NoSQL.Entities;

namespace Paraglider.Data.MongoDB
{
    public class WeddingComponentDataAccess
    {
        private const string COLLECTION_NAME = "wedding-components";
        private readonly IMongoCollection<WeddingComponent> _collection;

        public WeddingComponentDataAccess(IMongoDatabase mongoDB) 
        {
            _collection = mongoDB.GetCollection<WeddingComponent>(COLLECTION_NAME);
        }

        //add

        //add range

        //get all
        
        //get by id

        //update

        //delete

    }
}
