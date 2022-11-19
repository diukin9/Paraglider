﻿using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Paraglider.Domain.NoSQL.Entities;

namespace Paraglider.Data.MongoDB
{
    public class WeddingComponentDataAccess : DataAccess<WeddingComponent>
    {
        private const string COLLECTION_NAME = "wedding-components";

        public WeddingComponentDataAccess(IMongoClient client, IMongoDbSettings settings) 
            : base(client, settings, COLLECTION_NAME)
        {

        }
    }
}