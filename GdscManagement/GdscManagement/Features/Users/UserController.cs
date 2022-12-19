using GdscManagement.Database;
using GdscManagement.Features.Roles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GdscManagement.Features.Users;

[ApiController]
[Route("user")]

public class UserController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public UserController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<UserResponse>> Add(UserRequest request)
    {
        var memberRole = await _dbContext.Roles.Include(r => r.Users).FirstOrDefaultAsync(x => x.Title == "Member");
        if (memberRole is null)
            return NotFound("Member role does not exist!");

        var roles = new List<Role>
        {
            memberRole
        };
        
        var user = new User()
        {
            Id = Guid.NewGuid().ToString(),
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            FirstName = request.FirstName,
            LastName = request.Lastname,
            Email = request.Email,
            Roles = roles
            
        };
        
        user = (await _dbContext.Users.AddAsync(user)).Entity;
        await _dbContext.SaveChangesAsync();

        var res = new UserResponse
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Roles = user.Roles.Select(
                role => new RoleResponseForUser()
                {
                    Title = role.Title,
                    Description = role.Description
                })
        };
        
        return Created("user", res);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<UserResponse>>> Get()
    {
        var result = await _dbContext.Users.Include(x => x.Roles).Select(
            user => new UserResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Roles = user.Roles.Select(
                    role => new RoleResponseForUser()
                    {
                        Title = role.Title,
                        Description = role.Description
                    })
            }).ToListAsync(); 
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<UserResponse>> Get([FromRoute] string id)
    {
        var result = await _dbContext.Users.Include(user => user.Roles).FirstOrDefaultAsync(x => x.Id == id);
        if (result is null)
        {
            return NotFound("User not found!");
        }

        return Ok(new UserResponse
        {
            Id = result.Id,
            FirstName = result.FirstName,
            LastName = result.LastName,
            Email = result.Email,
            Roles = result.Roles.Select(
                role => new RoleResponseForUser
                {
                    Title = role.Title,
                    Description = role.Description
                })
        });

    }
}