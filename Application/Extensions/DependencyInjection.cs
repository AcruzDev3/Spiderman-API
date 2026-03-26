using Application.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Application.Services;

namespace LIB.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services) {
            services.AddScoped<ICrimeService, CrimeService>();
            services.AddScoped<ICriminalService, CriminalService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAddressService, AddressService>();

            return services;
        }
    }
}
