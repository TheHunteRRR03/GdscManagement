using Microsoft.EntityFrameworkCore;

namespace GdscManagement.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options){}
    
    
}