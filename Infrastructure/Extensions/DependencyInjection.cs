using Application.Interfaces;
using Application.Interfaces.IServices;
using Domain.Interfaces.IRepositories;
using Infrastructure.Repositories;
using Infrastructure.Security;
using Infrastructure.Services;
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
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ICriminalRiskLevelRepository, CriminalRiskLevelRepository>();

            services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();
            services.AddSingleton<IAzureImageService, AzureImageService>();
            services.AddSingleton<IJwtService, JwtService>();

            return services;
        }
    }
}
