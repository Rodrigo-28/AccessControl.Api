using AccessControlApi.Application.Dtos.Requests;
using AccessControlApi.Validators;
using FluentValidation;

namespace AccessControlApi.Extensions
{
    public static class ValidatorsExtension
    {
        public static IServiceCollection AddCustomValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<LoginValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateUserDto>();
            services.AddValidatorsFromAssemblyContaining<RegisterDto>();


            return services;
        }
    }
}
