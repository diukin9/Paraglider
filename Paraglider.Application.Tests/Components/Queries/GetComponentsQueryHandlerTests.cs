using FluentValidation;
using Paraglider.API.Features.Components.Queries;
using Paraglider.API.Tests.Common;
using Paraglider.Infrastructure.Common.Extensions;
using Xunit;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.API.Tests.Components.Queries;

public class GetComponentsQueryHandlerTests
{
    [Fact]
    public async Task When_CategoryIdIsEmpty_Expect_ValidationException()
    {
        // Arrange
        var request = new GetComponentsRequest(CategoryId: Guid.Empty, 15, 1);
        var validator = new GetComponentsRequestValidator();

        var handler = new GetComponentsQueryHandler(
            Fixtures.Components, 
            Fixtures.CategoryRepository, 
            validator);

        //Act
        var act = await handler.Handle(request, CancellationToken.None);

        //Assert
        Assert.False(act.IsOk);
        Assert.True(act.Exception is ValidationException);
        Assert.Equal(ExceptionMessages.ValidationError, act.GetMessage());
    }

    //TODO empty categoryId

    //TODO perpage == 0

    //TODO perPage < 0

    //TODO page = 0

    //TODO page < 0

    //TODO sorter key is not null and not valid

    //TODO different category id 

    //TODO with sort

    //TODO with descending sort

    //TODO with pagination
}
