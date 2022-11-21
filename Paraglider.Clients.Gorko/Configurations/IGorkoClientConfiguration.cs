using Paraglider.Clients.Gorko.Models;
using Paraglider.Clients.Gorko.Resources;

namespace Paraglider.Clients.Gorko.Configurations;

public interface IGorkoClientConfiguration
{
    public IUsersResource Users { get; }
    public IGorkoResource<Role> Roles { get; }
    public IGorkoResource<City> Cities { get; }
    public IGorkoResource<Restaurant> Restaurants { get; }

    public ICarsResource Cars { get; }
}