using System.Text.Json.Serialization;
using Paraglider.Infrastructure.Common.Interfaces;

namespace Paraglider.Application.DataTransferObjects;

public class UserComponentDTO : IDataTransferObject
{
    [JsonIgnore]
    public string ComponentId { get; set; } = null!;
    public ComponentDTO? Component { get; set; }
}
