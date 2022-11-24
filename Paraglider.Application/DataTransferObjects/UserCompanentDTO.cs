using Mapster;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common.Abstractions;
using System.Text.Json.Serialization;

namespace Paraglider.API.DataTransferObjects
{
    public class UserCompanentDTO : IDataTransferObject
    {
        [JsonIgnore]
        public Guid ComponentId { get; set; }
        public object? Component { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<UserComponent, UserCompanentDTO>()
                .Ignore(dest => dest.Component)
                .RequireDestinationMemberSource(true);
        }
    }
}
