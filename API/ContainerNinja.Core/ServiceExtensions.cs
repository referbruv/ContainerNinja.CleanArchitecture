using ContainerNinja.Contracts.Config;
using ContainerNinja.Contracts.Services;
using ContainerNinja.Core.Options;
using ContainerNinja.Core.Services;
using ContainerNinja.Core.Services.Jwt;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace ContainerNinja.Core
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtTokenConfig>(configuration.GetSection("JwtTokenConfig"))
                .AddSingleton(x => x.GetRequiredService<IOptions<JwtTokenConfig>>().Value);

            services.AddLogging(options =>
            {
                options.AddConsole();
            });

            services.AddMemoryCache();
            services.AddHttpContextAccessor();
            
            // register services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddSingleton<ICachingService, CachingService>();

            return services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddMediatR(Assembly.GetExecutingAssembly());
        }

        public static IServiceCollection AddSwaggerWithVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(setup =>
            {
                setup.DefaultApiVersion = new ApiVersion(1, 0);
                setup.AssumeDefaultVersionWhenUnspecified = true;
                setup.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });

            services.ConfigureOptions<ConfigureSwaggerOptions>()
                .AddSwaggerGen(options =>
                {
                    // for further customization
                    // options.OperationFilter<DefaultValuesFilter>();
                });

            return services;
        }

        public static IServiceCollection AddJwtBearerAuthentication(this IServiceCollection services)
        {
            services.ConfigureOptions<ConfigureJwtBearerOptions>()
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();

            return services;
        }
    }
}
