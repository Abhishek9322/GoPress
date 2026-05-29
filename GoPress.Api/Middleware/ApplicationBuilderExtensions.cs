using GoPress.Infrastructure.Data;
using GoPress.Infrastructure.Seed;
using Microsoft.EntityFrameworkCore;

namespace GoPress.Api.Middleware
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task<WebApplication> SeedDatabaseAsync(this WebApplication app)
        {
            using (var scope =
                app.Services.CreateScope())
            {
                var services =
                    scope.ServiceProvider;

                var context =
                    services.GetRequiredService
                        <ApplicationDbContext>();

                // Apply Migrations
                await context.Database
                    .MigrateAsync();

                // Seed Admin
                await AdminSeeder
                    .SeedAdminAsync(context);
            }

            return app;

        }
    }
}
