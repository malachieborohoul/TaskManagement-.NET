namespace Identity.Data;

public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<AppDbContext> options):base(options)
    {
        
    }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

}