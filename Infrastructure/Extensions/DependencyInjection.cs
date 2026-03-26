using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Infrastructure.Repositories;
using Infrastructure.Security;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services) {
            services.AddScoped<ICrimeRepository, CrimeRepository>();
            services.AddScoped<ICriminalRepository, CriminalRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();

            return services;
        }
    }
}
