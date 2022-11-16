using Paraglider.Domain.Enums;

namespace Paraglider.Domain.ValueObjects;

public class Price
{
    public readonly decimal? Min;
    public readonly decimal? Max;

    //for entity framework
    private Price() { }

    public Price(decimal value, IntervalType intervalType)
    {
        if (value < 0)
        {
            throw new ArgumentException("Price cannot be negative");
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

    public Price(int min, int max)
    {
        if (min <= 0 || max <= 0)
        {
            throw new ArgumentException("Price cannot be negative");
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
        if (Min.HasValue && Max.HasValue && Min.Value == Max.Value)
        {
            return $"{Min} руб.";
        }
        if (Min.HasValue && Max.HasValue)
        {
            return $"{Min}-{Max} руб.";
        }
        if (Min.HasValue)
        {
            return $"от {Min} руб";
        }
        if (Max.HasValue)
        {
            return $"до {Max} руб.";
        }

        throw new InvalidDataException();
    }
}
