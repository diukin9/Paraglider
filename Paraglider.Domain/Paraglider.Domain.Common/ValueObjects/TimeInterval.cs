using MongoDB.Bson.Serialization.Attributes;
using Paraglider.Domain.Common.Enums;

namespace Paraglider.Domain.Common.ValueObjects;

public class TimeInterval
{
    [BsonElement("start")]
    public TimeOnly? IntervalStart { get; set; }

    [BsonElement("end")]
    public TimeOnly? IntervalEnd { get; set; }

    //for entity framework
    //TODO попробовать потом сделать приватным
    public TimeInterval() { }

    public TimeInterval(TimeOnly intervalStart, TimeOnly intervalEnd)
    {
        IntervalStart = intervalStart;
        IntervalEnd = intervalEnd;
    }

    public TimeInterval(TimeOnly value, IntervalType type)
    {
        switch (type)
        {
            case IntervalType.From:
                IntervalStart = value;
                break;
            case IntervalType.To:
                IntervalEnd = value;
                break;
            default:
                IntervalStart = value;
                IntervalEnd = value;
                break;
        }
    }
}