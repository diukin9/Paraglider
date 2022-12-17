using Paraglider.Infrastructure.Common.Interfaces;
using System.Text.Json.Serialization;

namespace Paraglider.Application.DataTransferObjects;

public class TimeIntervalDTO : IDataTransferObject
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TimeOnly? IntervalStart { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TimeOnly? IntervalEnd { get; set; }
}
