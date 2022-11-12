using Paraglider.AspNetCore.Identity.Domain.Enums;

namespace Paraglider.AspNetCore.Identity.Domain.ValueObjects
{
    public class Time
    {
        public readonly TimeOfDay? IntervalStart;
        public readonly TimeOfDay? IntervalEnd;
        public readonly TimeOfDay? ExactTime;
        public readonly TimeType Type;

        public Time() { }

        public Time(TimeOfDay intervalStart, TimeOfDay intervalEnd)
        {
            if (intervalStart > TimeOfDay.MaxValue || intervalEnd > TimeOfDay.MaxValue)
            {
                throw new ArgumentException("The time cannot be more than 24 hours");
            }

            Type = TimeType.Interval;
            IntervalStart = intervalStart;
            IntervalEnd = intervalEnd;
        }

        public Time(TimeType type, TimeOfDay time)
        {
            if (time > TimeOfDay.MaxValue)
            {
                throw new ArgumentException("The time cannot be more than 24 hours");
            }

            if (type == TimeType.ExactTime)
            {
                ExactTime = time;
            }
            else if (type == TimeType.AfterThatTime)
            {
                IntervalStart = time;
            }
            else if (type == TimeType.BeforeThatTime)
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
}
