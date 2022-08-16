using ContainerNinja.Contracts.Data;
using ContainerNinja.Core.Data;
using ContainerNinja.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace ContainerNinja.Infrastructure
{
    public static class ServiceExtensions
    {
        private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            return services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            // services.AddSqlite<DatabaseContext>(configuration.GetConnectionString("DefaultConnection"), (options) =>
            // {
            //     options.MigrationsAssembly("ContainerNinja.Migrations");
            // });

            // services.AddDbContext(options =>
            // {
            //     options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            //     options.MigrationsAssembly("ContainerNinja.Migrations");
            // });

            services.AddNpgsql<DatabaseContext>(configuration.GetConnectionString("DefaultConnection"), (options) =>
            {
                options.MigrationsAssembly("ContainerNinja.Migrations");
            });

            return services;
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDatabaseContext(configuration).AddUnitOfWork();
        }
    }
}
