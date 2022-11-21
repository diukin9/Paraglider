using MongoDB.Bson.Serialization.Attributes;
using Paraglider.Infrastructure.Common.Enums;

namespace Paraglider.Infrastructure.Common;

public class TimeInterval
{
    [BsonElement("start")]
    public readonly TimeOnly? IntervalStart;

    [BsonElement("end")]
    public readonly TimeOnly? IntervalEnd;

    //for entity framework
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