using Paraglider.Domain.Enums;

namespace Paraglider.Domain.ValueObjects;

public class TimeInterval
{
    public readonly TimeOnly? IntervalStart;
    public readonly TimeOnly? IntervalEnd;

    //for entity framework
    private TimeInterval() { }

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
