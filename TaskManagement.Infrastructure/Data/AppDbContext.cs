using TaskManagement.Domain.Entity.Authentication;
using TaskManagement.Domain.Entity.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TaskManagement.Infrastructure.Data;

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
    public DbSet<Priority> Priorities { get; set; }
    public DbSet<Assignee> Assignees { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Assignee many-to-many relationship
        modelBuilder.Entity<Assignee>()
            .HasOne(a => a.User)
            .WithMany(u => u.Assignees)
            .HasForeignKey(a => a.UserId);

        modelBuilder.Entity<Assignee>()
            .HasOne(a => a.Tasks)
            .WithMany(t => t.Assignees)
            .HasForeignKey(a => a.TaskId);
        
        // One To Many
        modelBuilder.Entity<SubTask>()
            .HasOne(a => a.Tasks)
            .WithMany(t => t.SubTasks)
            .HasForeignKey(a => a.TaskId);
        
    }

    

}