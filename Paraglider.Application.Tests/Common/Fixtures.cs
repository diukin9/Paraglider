//using Mapster;
//using MapsterMapper;
//using Paraglider.Data;
//using Paraglider.Data.EntityFrameworkCore.Repositories;
//using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
//using Paraglider.Domain.NoSQL.Entities;
//using Paraglider.Infrastructure.Common.MongoDB;

//namespace Paraglider.API.Tests.Common;

//public static class Fixtures 
//{
//    public static readonly IMongoDataAccess<Component> Components;

//    public static readonly ApplicationDbContext Context; 


//    public static readonly IUserRepository UserRepository;

//    public static readonly ICategoryRepository CategoryRepository;

//    public static readonly CityRepository CityRepository;


//    public static readonly IMapper Mapper;

//    static Fixtures()
//    {
//        Context = ApplicationDbContextFactory.Create();
//        Components = ComponentDataAccessFactory.Create();

//        UserRepository = new UserRepository(Context);
//        CategoryRepository = new CategoryRepository(Context);
//        CityRepository = new CityRepository(Context);

//        //mapster
//        var config = TypeAdapterConfig.GlobalSettings;
//        config.Scan(typeof(Definitions.Mapping.MapsterDefinition).Assembly);
//        Mapper = new Mapper(config);
//    }
//}
