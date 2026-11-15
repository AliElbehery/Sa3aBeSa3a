using Microsoft.Extensions.DependencyInjection;
using Sa3aBeSa3a.Domain.Interfaces.Services;
using Sa3aBeSa3a.Application.Services;
using Sa3aBeSa3a.Domain.Interfaces;

namespace Sa3aBeSa3a.Application
{
    public static class DI
    {
        public static IServiceCollection AddApplication(
        this IServiceCollection services)
        {
            services.AddScoped<IHelpRequestServices, HelpRequestServices>();
            services.AddScoped<IOfferServices, OfferServices>();
            services.AddScoped<IAuthServices, AuthServices>();
            services.AddSingleton<ITokenServices, TokenServices>();
            return services;
        }
    }
}
