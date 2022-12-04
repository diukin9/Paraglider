using Microsoft.AspNetCore.Http;
using Moq;
using Paraglider.API.DataTransferObjects;
using Paraglider.API.Features.Categories.Queries;
using Paraglider.API.Tests.Common;
using Paraglider.Infrastructure.Common.Extensions;
using Xunit;
using static Paraglider.API.Tests.Common.StaticData;

namespace Paraglider.API.Tests.Categories.Queries;

public class GetUserCategoriesQueryHandlerTests
{
    [Fact]
    public async Task When_HandlerIsCalled_Expect_AllUserCategories()
    {
        //Arrange
        var mock = new Mock<IHttpContextAccessor>();
        mock.Setup(x => x.HttpContext!.User.Identity!.Name)
            .Returns(Username);

        var request = new GetUserCategoriesRequest();

        var handler = new GetUserCategoriesQueryHandler(
            Fixtures.CategoryRepository, 
            Fixtures.UserRepository, 
            mock.Object, 
            Fixtures.Mapper);

        var expected = Fixtures.Context.Users
            .Where(x => x.UserName == Username)
            .SingleOrDefault()
            ?.Planning.Categories
            .Count;

        //Act
        var act = await handler.Handle(request, CancellationToken.None);
        var collection = act.GetDataObject() as IEnumerable<CategoryDTO>;

        //Assert
        Assert.NotNull(collection);
        Assert.Equal(expected, collection.Count());
    }

    [Fact]
    public async Task When_HttpContextIsNull_Expect_ErrorInOperationResult()
    {
        //Arrange
        var mock = new Mock<IHttpContextAccessor>();
        mock.Setup(x => x.HttpContext)
            .Returns<IHttpContextAccessor>(default);

        var request = new GetUserCategoriesRequest();

        var handler = new GetUserCategoriesQueryHandler(
            Fixtures.CategoryRepository, 
            Fixtures.UserRepository, 
            mock.Object, 
            Fixtures.Mapper);

        //Act
        var act = await handler.Handle(request, CancellationToken.None);

        //Assert
        Assert.False(act.IsOk);
    }

    //TODO точно ли такое поведение, когда пользователь не авторизован
    [Fact]
    public async Task When_ThereIsNoAuthorizedUser_Expect_ErrorInOperationResult()
    {
        //Arrange
        var mock = new Mock<IHttpContextAccessor>();
        mock.Setup(x => x.HttpContext!.User.Identity!.Name)
            .Returns<string>(default);

        var request = new GetUserCategoriesRequest();

        var handler = new GetUserCategoriesQueryHandler(
            Fixtures.CategoryRepository, 
            Fixtures.UserRepository, 
            mock.Object, 
            Fixtures.Mapper);

        //Act
        var act = await handler.Handle(request, CancellationToken.None);

        //Assert
        Assert.False(act.IsOk);
    }
}
