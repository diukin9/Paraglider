//using Microsoft.EntityFrameworkCore;
//using Paraglider.Application.Features.Categories.Queries;
//using Paraglider.Application.Tests.Common;
//using Paraglider.Data;
//using Paraglider.Infrastructure.Common.Extensions;
//using Xunit;

//namespace Paraglider.Application.Tests.Categories.Queries;

//public class GetAllCategoriesQueryHandlerTests
//{
//    [Fact]
//    public async Task When_HandlerIsCalled_Expect_AllCategories()
//    {
//        //Arrange
//        var request = new GetAllCategoriesRequest();
//        var handler = new GetAllCategoriesQueryHandler(Fixtures.CategoryRepository, Fixtures.Mapper);
//        var expected = Fixtures.Context.Categories.Count();

//        //Act
//        var act = await handler.Handle(request, CancellationToken.None);

//        //Assert
//        Assert.Equal(expected, act.GetDataObject()!.Count());
//    }

//    [Fact]
//    public async Task When_ThereAreNotCategories_Expect_EmptyCollection()
//    {
//        //Arrange
//        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
//            .UseInMemoryDatabase(Guid.NewGuid().ToString())
//            .Options;

//        var context = new ApplicationDbContext(options);
//        var repository = new Data.EntityFrameworkCore.Repositories.CategoryRepository(context);

//        var request = new GetAllCategoriesRequest();
//        var handler = new GetAllCategoriesQueryHandler(repository, Fixtures.Mapper);
//        var expected = default(int);

//        //Act
//        var act = await handler.Handle(request, CancellationToken.None);

//        //Assert
//        Assert.Equal(expected, act.GetDataObject()!.Count());
//    }
//}
