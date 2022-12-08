//using Paraglider.API.Features.Components.Queries;
//using Paraglider.API.Tests.Common;
//using Paraglider.Infrastructure.Common.Extensions;
//using System.ComponentModel.DataAnnotations;
//using Xunit;
//using static Paraglider.Infrastructure.Common.AppData;

//namespace Paraglider.API.Tests.Components.Queries;

//public class GetComponentByIdQueryHandlerTests
//{
//    [Fact]
//    public async Task When_CategoryIdIsEmptyGuid_Expect_ValidationException()
//    {
//        // Arrange
//        var request = new GetComponentByIdRequest(Guid.Empty);
//        var handler = new GetComponentByIdQueryHandler(Fixtures.Components);

//        //Act
//        var act = await handler.Handle(request, CancellationToken.None);

//        //Assert
//        Assert.False(act.IsOk);
//        Assert.True(act.Exception is ValidationException);
//    }

//    [Fact]
//    public async Task When_IdNotExist_Expect_Null()
//    {
//        // Arrange
//        var request = new GetComponentByIdRequest(Guid.NewGuid());
//        var handler = new GetComponentByIdQueryHandler(Fixtures.Components);

//        //Act
//        var act = await handler.Handle(request, CancellationToken.None);

//        //Assert
//        Assert.False(act.IsOk);
//        Assert.Null(act.GetDataObject());
//    }

//    [Fact]
//    public async Task When_CategoryIdIsCorrect_Expect_Component()
//    {
//        // Arrange
//        var request = new GetComponentByIdRequest(StaticData.PhotographerComponentId);
//        var handler = new GetComponentByIdQueryHandler(Fixtures.Components);

//        //Act
//        var act = await handler.Handle(request, CancellationToken.None);

//        //Assert
//        Assert.True(act.IsOk);
//        Assert.NotNull(act.GetDataObject());
//    }
//}
