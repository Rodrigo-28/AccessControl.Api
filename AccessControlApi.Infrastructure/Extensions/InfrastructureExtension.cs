using AccessControlApi.Domian.Interfaces;
using AccessControlApi.Infrastructure.Repositories;
using AccessControlApi.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AccessControlApi.Infrastructure.Extensions
{
    public static class InfrastructureExtension
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRolRepository, RolRepository>();
            services.AddScoped<IPasswordEncryptionService, PasswordEncryptionService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();

            return services;
        }
    }
}
