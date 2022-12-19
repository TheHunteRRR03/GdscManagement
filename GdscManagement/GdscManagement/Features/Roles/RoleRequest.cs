using System.ComponentModel.DataAnnotations;

namespace GdscManagement.Features.Roles;

public class RoleRequest
{
    [Required] public string Title { get; set; }
    [Required] public string Description { get; set; }
    
}