using Paraglider.GorkoClient.Models;
using Paraglider.GorkoClient.Models.Enums;

namespace Paraglider.GorkoClient.Resources;

public interface ICarsResource
{
    IGorkoResource<Car> WithType(CarType carType);
}