//using Microsoft.EntityFrameworkCore;
//using Paraglider.Data;
//using Paraglider.Data.EntityFrameworkCore.Factories;
//using Paraglider.Domain.Common.ValueObjects;
//using Paraglider.Domain.RDB.Entities;
//using Paraglider.Domain.RDB.Enums;
//using static Paraglider.Application.Tests.Common.StaticData;

//namespace Paraglider.Application.Tests.Common;

//public class ApplicationDbContextFactory
//{
//    public static ApplicationDbContext Create()
//    {
//        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
//            .UseInMemoryDatabase(InMemoryTestDbName)
//            .Options;

//        var context = new ApplicationDbContext(options);

//        var user = UserFactory.Create(new UserData(
//            firstName: "Petr",
//            surname: "Diukin",
//            username: Username,
//            city: new City() { Id = DefaultCityId, Name = DefaultCityName },
//            email: "petr.dyukin@tmk-group.com",
//            emailConfirmed: true,
//            phoneNumber: "89502057422"));

//        context.Users.Add(user);

//        context.Categories.AddRange(
//            new Category() { Id = PhotographerCategoryId, Name = PhotographerCategoryName },
//            new Category() { Id = ConfectionerCategoryId, Name = ConfectionerCategoryName },
//            new Category() { Id = VideographerCategoryId, Name = VideographerCategoryName }
//        );

//        context.UserComponents.Add(new UserComponent()
//        {
//            ComponentId = PhotographerCategoryId,
//            UserId = user.Id
//        });

//        context.PlanningComponents.Add(new PlanningComponent()
//        {
//            PlanningId = user.PlanningId,
//            CategoryId = PhotographerCategoryId,
//            ComponentId = PhotographerComponentId,
//            ComponentDesc = new ComponentDesc()
//            {
//                Status = ComponentStatus.Agreed,
//                TimeInterval = new TimeInterval(new TimeOnly(16, 0, 0), new TimeOnly(4, 0, 0))
//            }
//        });

//        context.SaveChanges();
//        return context;
//    }

//    public static void Destroy(ApplicationDbContext context)
//    {
//        context.Database.EnsureDeleted();
//        context.Dispose();
//    }
//}
