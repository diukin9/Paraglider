namespace Paraglider.Domain.ValueObjects;

public class Price
{
    public readonly decimal? Min;
    public readonly decimal? Max;

    public Price() { }

    public Price(decimal? min = null, decimal? max = null)
    {
        if (!min.HasValue && !max.HasValue)
        {
            throw new ArgumentException("Empty parameters were passed");
        }

        if (min.HasValue && max.HasValue && min > max)
        {
            throw new ArgumentException("'Min' cannot be higher than 'Max'");
        }

        if (min.HasValue && min.Value < 0 || max.HasValue && max < 0)
        {
            throw new ArgumentException("Price cannot be negative");
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
