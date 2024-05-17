using Domain.Entity.Authentication;
using Domain.Entity.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext:IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
    {
        
    }
    
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Status> Status { get; set; }
    public DbSet<Tasks> Tasks { get; set; }
    public DbSet<SubTask> SubTasks { get; set; }
    public DbSet<Activity> Activities { get; set; }
}