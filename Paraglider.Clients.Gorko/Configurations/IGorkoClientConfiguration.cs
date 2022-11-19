using Paraglider.GorkoClient.Models;
using Paraglider.GorkoClient.Resources;

namespace Paraglider.GorkoClient.Configurations;

public interface IGorkoClientConfiguration
{
    public IUsersResource Users { get; }
    public IGorkoResource<Role> Roles { get; }
    public IGorkoResource<City> Cities { get; }
    public IGorkoResource<Restaurant> Restaurants { get; }

    public ICarsResource Cars { get; }
}