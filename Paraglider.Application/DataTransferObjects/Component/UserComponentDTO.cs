using System.Text.Json.Serialization;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.API.DataTransferObjects;

public class UserComponentDTO : IDataTransferObject
{
    [JsonIgnore]
    public Guid ComponentId { get; set; }
    public ComponentDTO? Component { get; set; }
}
