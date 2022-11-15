namespace Paraglider.Domain.ValueObjects;

public class Capacity
{
    public readonly int? Min;
    public readonly int? Max;

    Capacity() { }

    public Capacity(int? min = null, int? max = null)
    {
        if (!min.HasValue && !max.HasValue)
        {
            throw new ArgumentException("Empty parameters were passed");
        }

        if (min.HasValue && max.HasValue && min > max)
        {
            throw new ArgumentException("'Min' cannot be higher than 'Max'");
        }

        Min = min;
        Max = max;
    }

    public override string ToString()
    {
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
