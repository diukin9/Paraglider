namespace Paraglider.API.Tests.Common;

public static class StaticData
{
    public const string MongoTestDbName = "MongoTestDatabase";
    public const string InMemoryTestDbName = "InMemoryTestDatabase";

    public static readonly Guid VideographComponentId = Guid.NewGuid();
    public static readonly Guid PhotographerComponentId = Guid.NewGuid();

    public const string Username = "diukin";

    public static readonly Guid DefaultCityId = Guid.NewGuid();
    public const string DefaultCityName = "Екатеринбург";

    public static readonly Guid VideographerCategoryId = Guid.NewGuid();
    public const string VideographerCategoryName = "Видеографы";

    public static readonly Guid PhotographerCategoryId = Guid.NewGuid();
    public const string PhotographerCategoryName = "Фотографы";

    public static readonly Guid ConfectionerCategoryId = Guid.NewGuid();
    public const string ConfectionerCategoryName = "Кондитеры";
}
