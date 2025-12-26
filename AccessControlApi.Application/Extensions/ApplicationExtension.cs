using AccessControlApi.Application.Interfaces;
using AccessControlApi.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AccessControlApi.Application.Extensions
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAuthService, AuthService>();
            return services;
        }
    }
}
