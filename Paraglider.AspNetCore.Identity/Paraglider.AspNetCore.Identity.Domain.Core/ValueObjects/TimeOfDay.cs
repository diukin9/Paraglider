namespace Paraglider.AspNetCore.Identity.Domain.ValueObjects
{
    public class TimeOfDay
    {
        public int Hour { get; private set; }
        public int Minute { get; private set; }
        public int Second { get; private set; }

        public TimeOfDay(int hour = 0, int minute = 0, int second = 0)
        {
            if (hour > 24)
            {
                throw new ArgumentException("The 'Hour' value cannot be higher than 24");
            }
            if (minute > 60)
            {
                throw new ArgumentException("The 'Minute' value cannot be higher than 60");
            }
            if (second > 60)
            {
                throw new ArgumentException("The 'Second' value cannot be higher than 60");
            }

            if (hour < 0 || minute < 0 || second < 0)
            {
                throw new ArgumentException("Time cannot be negative");
            }

            Hour = hour;
            Minute = minute;
            Second = second;
        }

        public double TotalHours => TotalSeconds / (1d * 3600);
        public double TotalMinutes => TotalSeconds / 60d;
        public int TotalSeconds => Hour * 3600 + Minute * 60 + Second;

        public static TimeOfDay MinValue => new(0, 0, 0);
        public static TimeOfDay MaxValue => new(23, 59, 59);

        public bool Equals(TimeOfDay other)
        {
            return other != null && TotalSeconds == other.TotalSeconds;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false; 
            var td = obj as TimeOfDay;
            return td != null && Equals(td);
        }

        public override int GetHashCode()
        {
            return TotalSeconds;
        }

        public override string ToString()
        {
            return ToString("HH:mm:ss");
        }

        public string ToString(string format)
        {
            var now = DateTime.Now;
            var dt = new DateTime(now.Year, now.Month, now.Day, Hour, Minute, Second);
            return dt.ToString(format);
        }

        public static bool operator !=(TimeOfDay t1, TimeOfDay t2)
        {
            return t1 is null || t2 is null || t1.TotalSeconds != t2.TotalSeconds;
        }

        public static bool operator <(TimeOfDay t1, TimeOfDay t2)
        {
            return t1 is null || t2 is null || t1.TotalSeconds < t2.TotalSeconds;
        }

        public static bool operator <=(TimeOfDay t1, TimeOfDay t2)
        {
            return t1 is null || t2 is null || t1 == t2 || t1.TotalSeconds <= t2.TotalSeconds;
        }

        public static bool operator ==(TimeOfDay t1, TimeOfDay t2)
        {
            return t1 is null || t2 is null || t1.Equals(t2);
        }

        public static bool operator >(TimeOfDay t1, TimeOfDay t2)
        {
            return t1 is null || t2 is null || t1.TotalSeconds > t2.TotalSeconds;
        }

        public static bool operator >=(TimeOfDay t1, TimeOfDay t2)
        {
            return t1 is null || t2 is null || t1.TotalSeconds >= t2.TotalSeconds;
        }
    }
}
