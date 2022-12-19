using GdscManagement.Features.Roles;

namespace GdscManagement.Features.Users;

public class UserResponse
{
    public string Id { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Email { get; set; }
    
    public IEnumerable<RoleResponseForUser> Roles { get; set; }
}