using MongoDB.Bson.Serialization.Attributes;

namespace Paraglider.Domain.RDB.ValueObjects;

public class TimeInterval
{
    [BsonElement("start")]
    public TimeOnly? IntervalStart { get; set; }

    [BsonElement("end")]
    public TimeOnly? IntervalEnd { get; set; }
}