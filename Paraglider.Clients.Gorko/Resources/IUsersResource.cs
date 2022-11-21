using Paraglider.Clients.Gorko.Models;
using Paraglider.Clients.Gorko.Models.Enums;

namespace Paraglider.Clients.Gorko.Resources;

public interface IUsersResource
{
    IGorkoResource<User> WithRole(UserRole userRole);
}