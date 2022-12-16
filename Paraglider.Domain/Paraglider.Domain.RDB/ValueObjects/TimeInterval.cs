using MongoDB.Bson.Serialization.Attributes;

namespace Paraglider.Domain.RDB.ValueObjects;

public class TimeInterval
{
    public TimeOnly? IntervalStart { get; set; }

    public TimeOnly? IntervalEnd { get; set; }
}