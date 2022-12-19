using GdscManagement.Database;
using Microsoft.AspNetCore.Mvc;

namespace GdscManagement.Features.Roles;

[ApiController]
[Route("role")]

public class RoleController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public RoleController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Role>> AddRole(RoleRequest request)
    {
        var role = new Role()
        {
            Id = Guid.NewGuid().ToString(),
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            Title = request.Title,
            Description = request.Description
        };

        var result = await _dbContext.Roles.AddAsync(role);
        await _dbContext.SaveChangesAsync();

        return Created("role", result.Entity);
    }
}