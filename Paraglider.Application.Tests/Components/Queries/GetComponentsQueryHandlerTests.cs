using FluentValidation;
using Paraglider.API.Features.Components.Queries;
using Paraglider.API.Tests.Common;
using Paraglider.Infrastructure.Common.Extensions;
using Xunit;
using static Paraglider.Infrastructure.Common.AppData;
using static Paraglider.API.Tests.Common.StaticData;

namespace Paraglider.API.Tests.Components.Queries;

public class GetComponentsQueryHandlerTests
{
    [Fact]
    public async Task When_CategoryIdIsEmpty_Expect_ValidationException()
    {
        // Arrange
        var request = new GetComponentsRequest(CategoryId: Guid.Empty, 15, 1);
        var validator = new GetComponentsRequestValidator();
        var handler = new GetComponentsQueryHandler(Fixtures.Components, validator);

        //Act
        var act = await handler.Handle(request, CancellationToken.None);

        //Assert
        Assert.False(act.IsOk);
        Assert.True(act.Exception is ValidationException);
        Assert.Equal(ExceptionMessages.ValidationError, act.GetMessage());
    }

    [Fact]
    public async Task When_PerPageIsZero_Expect_ValidationException()
    {
        // Arrange
        var request = new GetComponentsRequest(VideographComponentId, PerPage: 0, 1);
        var validator = new GetComponentsRequestValidator();
        var handler = new GetComponentsQueryHandler(Fixtures.Components, validator);

        //Act
        var act = await handler.Handle(request, CancellationToken.None);

        //Assert
        Assert.False(act.IsOk);
        Assert.True(act.Exception is ValidationException);
        Assert.Equal(ExceptionMessages.ValidationError, act.GetMessage());
    }

    [Fact]
    public async Task When_PerPageLessThanZero_Expect_ValidationException()
    {
        // Arrange
        var request = new GetComponentsRequest(VideographComponentId, PerPage: -10, 1);
        var validator = new GetComponentsRequestValidator();
        var handler = new GetComponentsQueryHandler(Fixtures.Components, validator);

        //Act
        var act = await handler.Handle(request, CancellationToken.None);

        //Assert
        Assert.False(act.IsOk);
        Assert.True(act.Exception is ValidationException);
        Assert.Equal(ExceptionMessages.ValidationError, act.GetMessage());
    }

    [Fact]
    public async Task When_PageIsZero_Expect_ValidationException()
    {
        // Arrange
        var request = new GetComponentsRequest(VideographComponentId, 15, Page: 0);
        var validator = new GetComponentsRequestValidator();
        var handler = new GetComponentsQueryHandler(Fixtures.Components, validator);

        //Act
        var act = await handler.Handle(request, CancellationToken.None);

        //Assert
        Assert.False(act.IsOk);
        Assert.True(act.Exception is ValidationException);
        Assert.Equal(ExceptionMessages.ValidationError, act.GetMessage());
    }

    [Fact]
    public async Task When_PageLessThenZero_Expect_ValidationException()
    {
        // Arrange
        var request = new GetComponentsRequest(VideographComponentId, 15, Page: -10);
        var validator = new GetComponentsRequestValidator();
        var handler = new GetComponentsQueryHandler(Fixtures.Components, validator);

        //Act
        var act = await handler.Handle(request, CancellationToken.None);

        //Assert
        Assert.False(act.IsOk);
        Assert.True(act.Exception is ValidationException);
        Assert.Equal(ExceptionMessages.ValidationError, act.GetMessage());
    }

    [Fact]
    public async Task When_HandlerIsCalled_Expect_EqualToPerPageCount()
    {
        // Arrange
        var rnd = new Random();
        var componentCount = (int)await Fixtures.Components.CountAsync();
        var perPage = rnd.Next(1, componentCount);

        var request = new GetComponentsRequest(VideographerCategoryId, perPage, 1);
        var validator = new GetComponentsRequestValidator();
        var handler = new GetComponentsQueryHandler(Fixtures.Components, validator);

        var expected = (await Fixtures.Components
            .FindAsync(_ => _.Category.Id == VideographerCategoryId))
            .Count;

        //Act
        var act = await handler.Handle(request, CancellationToken.None);
        var collection = act.GetDataObject() as IEnumerable<object>;

        //Assert
        Assert.NotNull(collection);
        Assert.Equal(expected, collection.Count());
    }

    //TODO pagination
    //TODO different category id 

    //TODO sorter key is not null and not valid

    //TODO with sort

    //TODO with descending sort
}
