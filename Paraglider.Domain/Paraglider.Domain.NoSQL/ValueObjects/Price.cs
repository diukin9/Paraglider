using MongoDB.Bson.Serialization.Attributes;
using Paraglider.Domain.Common.Enums;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.Domain.NoSQL.ValueObjects;

public class Price
{
    [BsonIgnoreIfNull]
    [BsonElement("min")]
    public decimal? Min { get; set; }

    [BsonIgnoreIfNull]
    [BsonElement("max")]
    public decimal? Max { get; set; }

    //for deserialize
    //TODO попробовать потом сделать приватным
    public Price() { }

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
