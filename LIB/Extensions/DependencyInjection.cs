using LIB.Interfaces.IManagers;
using LIB.Interfaces.IRepositories;
using LIB.Managers;
using LIB.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace LIB.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services) {
            services.AddScoped<ICrimeManager, CrimeManager>();
            services.AddScoped<ICriminalManager, CriminalManager>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IAddressManager, AddressManager>();

            return services;
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services) {
            services.AddScoped<ICrimeRepository, CrimeRepository>();
            services.AddScoped<ICriminalRepository, CriminalRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();

            return services;
        }
    }
}
