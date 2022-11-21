using Paraglider.Clients.Gorko.Models;
using Paraglider.Clients.Gorko.Models.Enums;

namespace Paraglider.Clients.Gorko.Resources;

public interface ICarsResource
{
    IGorkoResource<Car> WithType(CarType carType);
}