using GdscManagement.Base;
using GdscManagement.Features.Users;

namespace GdscManagement.Features.Roles;

public class Role : Model
{
    public string Title { get; set; }
    public string Description { get; set; }
    public IEnumerable<User> Users { get; set; }
}