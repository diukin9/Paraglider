using Paraglider.Domain.NoSQL.Enums;

namespace Paraglider.Domain.NoSQL.ValueObjects;

public class Capacity
{
    public readonly int? Min;
    public readonly int? Max;

    //for entity framework
    private Capacity() { }

    public Capacity(int value, IntervalType intervalType)
    {
        if (value < 0)
        {
            throw new ArgumentException("Capacity cannot be negative");
        }

        switch (intervalType)
        {
            case IntervalType.From:
                Min = value;
                break;
            case IntervalType.To:
                Max = value;
                break;
            default:
                Min = value;
                Max = value;
                break;
        }
    }

    public Capacity(int min, int max)
    {
        if (min <= 0 || max <= 0)
        {
            throw new ArgumentException("Capacity cannot be negative");
        }

        if (min > max)
        {
            throw new ArgumentException("'Min' cannot be higher than 'Max'");
        }

        Min = min;
        Max = max;
    }

    public override string ToString()
    {
        if (Min.HasValue && Max.HasValue && Min == Max)
        {
            return $"{Max} чел.";
        }
        if (Min.HasValue && Max.HasValue)
        {
            return $"{Min}-{Max} чел.";
        }
        if (Min.HasValue)
        {
            return $"от {Min} чел.";
        }
        if (Max.HasValue)
        {
            return $"до {Max} чел.";
        }

        throw new InvalidDataException();
    }
}