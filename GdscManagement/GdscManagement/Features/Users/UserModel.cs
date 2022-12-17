using System.ComponentModel.DataAnnotations;

namespace GdscManagement.Features.Users;

public class UserModel
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    [EmailAddress]
    public string Email { get; set; }
    
    public IEnumerable<string> Roles { get; set; }
}