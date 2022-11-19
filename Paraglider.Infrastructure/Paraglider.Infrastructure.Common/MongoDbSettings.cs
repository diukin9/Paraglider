using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.Infrastructure.Common;

public class MongoDbSettings : IMongoDbSettings
{
    public string DatabaseName { get; set; } = null!;
}
