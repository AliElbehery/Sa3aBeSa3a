using Microsoft.Extensions.DependencyInjection;

namespace Sa3aBeSa3a.Application
{
    public static class DI
    {
        public static IServiceCollection AddApplication(
        this IServiceCollection services)
        {
            // Register Application services here

            return services;
        }
    }
}
