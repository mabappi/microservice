using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Core.IdentityProvider.Extensions
{
    public static class MigrateDbContext
    {
        public static IApplicationBuilder MigrateDatabase<TDbContext>(
            this IApplicationBuilder app)
            where TDbContext : DbContext
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<TDbContext>().Database.Migrate();
            }

            return app;
        }
    }
}
