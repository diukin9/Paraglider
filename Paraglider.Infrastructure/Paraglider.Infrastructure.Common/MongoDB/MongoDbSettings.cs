namespace Paraglider.Infrastructure.Common.MongoDB;

public class MongoDbSettings : IMongoDbSettings
{
    public string DatabaseName { get; set; } = null!;
}
