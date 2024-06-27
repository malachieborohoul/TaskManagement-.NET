using Microsoft.EntityFrameworkCore;

using TaskManagement.Infrastructure.Data;

namespace TaskManagement.API
{
    public static class MigrationExtensions
    {
        public static void ApplyMigration(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<Program>>();

                try
                {
                    var dbContext = services.GetRequiredService<AppDbContext>();
                    logger.LogInformation("Applying database migrations...");
                    dbContext.Database.Migrate();
                    logger.LogInformation("Database migrations applied successfully.");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while applying database migrations.");
                }
            }
        }
    }
}