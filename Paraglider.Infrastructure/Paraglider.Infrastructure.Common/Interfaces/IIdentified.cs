namespace Paraglider.Infrastructure.Common.Interfaces;

public interface IIdentified<T>
{
    public T Id { get; set; }
}