using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess
{
    public static class ConfigureDbContextExtensions
    {
        public static void ConfigureDbContext(this IServiceCollection services, string databaseName)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase(databaseName: databaseName),
                contextLifetime: ServiceLifetime.Scoped,
                optionsLifetime: ServiceLifetime.Scoped
            );
        }
    }
}