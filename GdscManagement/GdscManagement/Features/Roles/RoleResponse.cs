using GdscManagement.Features.Users;

namespace GdscManagement.Features.Roles;

public class RoleResponse
{
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public IEnumerable<UserResponse> Users { get; set; }
}