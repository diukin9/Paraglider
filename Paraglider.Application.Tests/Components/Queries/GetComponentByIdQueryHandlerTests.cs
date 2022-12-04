using FluentValidation;
using Paraglider.API.Features.Components.Queries;
using Paraglider.API.Tests.Common;
using Paraglider.Infrastructure.Common.Extensions;
using Xunit;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.API.Tests.Components.Queries;

public class GetComponentByIdQueryHandlerTests
{
    [Fact]
    public async Task When_CategoryIdIsEmptyGuid_Expect_ValidationException()
    {
        // Arrange
        var request = new GetComponentByIdRequest(Guid.Empty);
        var validator = new GetComponentByIdRequestValidator();
        var handler = new GetComponentByIdQueryHandler(Fixtures.Components, validator);

        //Act
        var act = await handler.Handle(request, CancellationToken.None);

        //Assert
        Assert.False(act.IsOk);
        Assert.True(act.Exception is ValidationException);
        Assert.Equal(ExceptionMessages.ValidationError, act.GetMessage());
    }

    [Fact]
    public async Task When_IdNotExist_Expect_Null()
    {
        // Arrange
        var request = new GetComponentByIdRequest(Guid.NewGuid());
        var validator = new GetComponentByIdRequestValidator();
        var handler = new GetComponentByIdQueryHandler(Fixtures.Components, validator);

        //Act
        var act = await handler.Handle(request, CancellationToken.None);

        //Assert
        Assert.False(act.IsOk);
        Assert.Null(act.GetDataObject());
    }

    [Fact]
    public async Task When_CategoryIdIsCorrect_Expect_Component()
    {
        // Arrange
        var request = new GetComponentByIdRequest(StaticData.PhotographerComponentId);
        var validator = new GetComponentByIdRequestValidator();
        var handler = new GetComponentByIdQueryHandler(Fixtures.Components, validator);

        //Act
        var act = await handler.Handle(request, CancellationToken.None);

        //Assert
        Assert.True(act.IsOk);
        Assert.NotNull(act.GetDataObject());
    }
}
