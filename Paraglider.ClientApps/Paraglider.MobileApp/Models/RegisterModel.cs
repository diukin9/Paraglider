using System.ComponentModel.DataAnnotations;

namespace Paraglider.MobileApp.Models;

public class RegisterModel
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public Guid CityId { get; set; }
}
