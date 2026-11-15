using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sa3aBeSa3a.Infrastructure.Data;
using Sa3aBeSa3a.Infrastructure.Repositories;
using Sa3aBeSa3a.Domain.Interfaces.Repositries;

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
            services.AddScoped<IHelpRequestRepository, HelpRequestRepository>();
            services.AddScoped<IOfferRepository, OfferRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();

            return services;
        }
    }
}
