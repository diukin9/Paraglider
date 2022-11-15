using Paraglider.Domain.Enums;

namespace Paraglider.Domain.ValueObjects;

public class TimeStamp
{
    public readonly TimeOfDay? IntervalStart;
    public readonly TimeOfDay? IntervalEnd;
    public readonly TimeOfDay? ExactTime;
    public readonly TimeStampType Type;

    public TimeStamp() { }

    public TimeStamp(TimeOfDay intervalStart, TimeOfDay intervalEnd)
    {
        if (intervalStart > TimeOfDay.MaxValue || intervalEnd > TimeOfDay.MaxValue)
        {
            throw new ArgumentException("The time cannot be more than 24 hours");
        }

        Type = TimeStampType.Interval;
        IntervalStart = intervalStart;
        IntervalEnd = intervalEnd;
    }

    public TimeStamp(TimeStampType type, TimeOfDay time)
    {
        if (time > TimeOfDay.MaxValue)
        {
            throw new ArgumentException("The time cannot be more than 24 hours");
        }

        if (type == TimeStampType.ExactTime)
        {
            ExactTime = time;
        }
        else if (type == TimeStampType.AfterThatTime)
        {
            IntervalStart = time;
        }
        else if (type == TimeStampType.BeforeThatTime)
        {
            IntervalEnd = time;
        }
        else
        {
            throw new ArgumentException();
        }

        Type = type;
    }
}
