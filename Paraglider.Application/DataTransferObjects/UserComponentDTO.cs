using System.Text.Json.Serialization;
using Mapster;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.API.DataTransferObjects;

public class UserComponentDTO : IDataTransferObject
{
    [JsonIgnore]
    public Guid ComponentId { get; set; }
    public object? Component { get; set; }

    public void Register(TypeAdapterConfig config)
    {
#pragma warning disable CS8603 // Возможно, возврат ссылки, допускающей значение NULL.

        config.NewConfig<UserComponent, UserComponentDTO>()
            .Ignore(dest => dest.Component)
            .RequireDestinationMemberSource(true);

#pragma warning restore CS8603 // Возможно, возврат ссылки, допускающей значение NULL.
    }
}
