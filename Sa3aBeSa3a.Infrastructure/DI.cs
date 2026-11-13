using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sa3aBeSa3a.Infrastructure.Data;

namespace Sa3aBeSa3a.Infrastructure
{
    public static class DI
    {
        public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string connectionString)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            return services;
        }
    }
}
