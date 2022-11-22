using MongoDB.Bson.Serialization.Attributes;
using Paraglider.Domain.Common.Enums;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.Domain.NoSQL.ValueObjects;

public class Capacity
{
    [BsonElement("min")]
    public readonly int? Min;

    [BsonElement("max")]
    public readonly int? Max;

    public Capacity(int value, IntervalType intervalType)
    {
        if (value < 0)
        {
            throw new ArgumentException(ExceptionMessages.CannotBeNegative(nameof(Capacity)));
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
            throw new ArgumentException(ExceptionMessages.CannotBeNegative(nameof(Capacity)));
        }

        if (min > max)
        {
            throw new ArgumentException(ExceptionMessages.CannotBeHigherThan(nameof(Min), nameof(Max)));
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