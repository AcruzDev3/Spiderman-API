using Application.Interfaces;
using Application.Interfaces.IServices;
using Application.Services;
using Domain.Interfaces.IRepositories;
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
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ICriminalRiskLevelRepository, CriminalRiskLevelRepository>();

            services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
            services.AddScoped<IAzureImageService, AzureImageService>();
            services.AddScoped<IJwtService, JwtService>();

            return services;
        }
    }
}
