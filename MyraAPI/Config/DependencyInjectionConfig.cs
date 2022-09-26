using FluentValidation.AspNetCore;
using Myra.Application;
using Myra.Application.Interfaces;
using Myra.Application.Services;
using Myra.Application.Utils;
using Myra.Application.Validators;
using Myra.Domain.Interfaces.Repositories;
using Myra.Infra.Context;
using Myra.Infra.Repositories;
using Myra.Infra.UnitOfWork;
using System.Reflection;

namespace Myra.API.Config
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection SolveDependencies(this IServiceCollection services)
        {
            //services
            services.AddScoped<ApplicationDbContext>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ITokenService, TokenService>();


            //repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddFluentValidation(fv =>
            {
                fv.AutomaticValidationEnabled = false;
                fv.RegisterValidatorsFromAssemblyContaining<UserValidator>(
                   asr => !(asr.ValidatorType.GetCustomAttribute<IgnoreInjectionAttribute>()?.Ignore ?? false));
            });


            return services;
        }
    }
}
