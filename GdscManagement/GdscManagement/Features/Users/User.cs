using GdscManagement.Base;
using GdscManagement.Features.Roles;

namespace GdscManagement.Features.Users;

public class User : Model
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Email { get; set; }
    
    public IEnumerable<Role> Roles { get; set; }
}