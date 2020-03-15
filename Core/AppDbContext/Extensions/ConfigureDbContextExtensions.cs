using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Core.AppDbContext
{
    public static class ConfigureDbContextExtensions
    {
        public static void ConfigureInMemoryDbContext<T>(
            this IServiceCollection services,
            string databaseName
        ) where T : DbContext
        {
            services.AddDbContext<T>(options =>
                options.UseInMemoryDatabase(databaseName: databaseName),
                contextLifetime: ServiceLifetime.Scoped,
                optionsLifetime: ServiceLifetime.Scoped
            );
        }
    }
}