using System.ComponentModel.DataAnnotations;
using GdscManagement.Features.Roles;

namespace GdscManagement.Features.Users;

public class UserRequest
{
    [Required] public string FirstName { get; set; }
    
    [Required] public string Lastname { get; set; }
    
    [EmailAddress]
    [Required] public string Email { get; set; }
}