using Paraglider.API.Features.Components.Queries;
using Paraglider.API.Tests.Common;
using Paraglider.Infrastructure.Common.Extensions;
using System.ComponentModel.DataAnnotations;
using Xunit;
using static Paraglider.API.Tests.Common.StaticData;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.API.Tests.Components.Queries;

public class GetComponentsQueryHandlerTests
{
    [Fact]
    public async Task When_CategoryIdIsEmpty_Expect_ValidationException()
    {
        // Arrange
        var request = new GetComponentsRequest(Guid.Empty, 15, 1);
        var handler = new GetComponentsQueryHandler(Fixtures.Components);

        //Act
        var act = await handler.Handle(request, CancellationToken.None);

        //Assert
        Assert.False(act.IsOk);
        Assert.True(act.Exception is ValidationException);
    }

    [Fact]
    public async Task When_PerPageIsZero_Expect_ValidationException()
    {
        // Arrange
        var request = new GetComponentsRequest(VideographComponentId, 0, 1);
        var handler = new GetComponentsQueryHandler(Fixtures.Components);

        //Act
        var act = await handler.Handle(request, CancellationToken.None);

        //Assert
        Assert.False(act.IsOk);
        Assert.True(act.Exception is ValidationException);
    }

    [Fact]
    public async Task When_PerPageLessThanZero_Expect_ValidationException()
    {
        // Arrange
        var request = new GetComponentsRequest(VideographComponentId, -10, 1);
        var handler = new GetComponentsQueryHandler(Fixtures.Components);

        //Act
        var act = await handler.Handle(request, CancellationToken.None);

        //Assert
        Assert.False(act.IsOk);
        Assert.True(act.Exception is ValidationException);
    }

    [Fact]
    public async Task When_PageIsZero_Expect_ValidationException()
    {
        // Arrange
        var request = new GetComponentsRequest(VideographComponentId, 15, 0);
        var handler = new GetComponentsQueryHandler(Fixtures.Components);

        //Act
        var act = await handler.Handle(request, CancellationToken.None);

        //Assert
        Assert.False(act.IsOk);
        Assert.True(act.Exception is ValidationException);
    }

    [Fact]
    public async Task When_PageLessThenZero_Expect_ValidationException()
    {
        // Arrange
        var request = new GetComponentsRequest(VideographComponentId, 15, -10);
        var handler = new GetComponentsQueryHandler(Fixtures.Components);

        //Act
        var act = await handler.Handle(request, CancellationToken.None);

        //Assert
        Assert.False(act.IsOk);
        Assert.True(act.Exception is ValidationException);
    }

    [Fact]
    public async Task When_HandlerIsCalled_Expect_EqualToPerPageCount()
    {
        // Arrange
        var rnd = new Random();
        var componentCount = (int)await Fixtures.Components.CountAsync();
        var perPage = rnd.Next(1, componentCount);

        var request = new GetComponentsRequest(VideographerCategoryId, perPage, 1);
        var handler = new GetComponentsQueryHandler(Fixtures.Components);

        var expected = (await Fixtures.Components
            .FindAsync(_ => _.Category.Id == VideographerCategoryId))
            .Count;

        //Act
        var act = await handler.Handle(request, CancellationToken.None);

        //Assert
        Assert.Equal(expected, act.GetDataObject()!.Count());
    }

    //TODO pagination
    //TODO different category id 

    //TODO sorter key is not null and not valid

    //TODO with sort

    //TODO with descending sort
}
