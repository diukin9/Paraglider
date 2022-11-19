using Paraglider.GorkoClient.Models;
using Paraglider.GorkoClient.Models.Enums;

namespace Paraglider.GorkoClient.Resources;

public interface IUsersResource
{
    IGorkoResource<User> WithRole(UserRole userRole);
}