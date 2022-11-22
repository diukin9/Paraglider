using MongoDB.Bson.Serialization.Attributes;
using Paraglider.Domain.Common.Enums;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.Domain.NoSQL.ValueObjects;

public class Price
{
    [BsonElement("min")]
    public readonly decimal? Min;

    [BsonElement("max")]
    public readonly decimal? Max;

    public Price(decimal value, IntervalType intervalType)
    {
        if (value < 0)
        {
            throw new ArgumentException(ExceptionMessages.CannotBeNegative(nameof(Price)));
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
        if (min < 0 || max < 0)
        {
            throw new ArgumentException(ExceptionMessages.CannotBeNegative(nameof(Price)));
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
