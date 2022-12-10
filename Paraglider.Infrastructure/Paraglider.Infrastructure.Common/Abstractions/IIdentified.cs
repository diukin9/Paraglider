namespace Paraglider.Infrastructure.Common.Abstractions;

public interface IIdentified<T>
{
    public T Id { get; set; }
}