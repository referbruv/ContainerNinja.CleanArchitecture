using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ContainerNinja.Infrastructure;
using ContainerNinja.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Serilog;

namespace ContainerNinja
{
    public class Startup
    {
        private readonly ILogger _serilogLogger;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _serilogLogger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                //.WriteTo.Console()
                //.WriteTo.MSSqlServer(
                //    configuration.GetConnectionString("LoggingDbConnection"),
                //    sinkOptions: new Serilog.Sinks.MSSqlServer.MSSqlServerSinkOptions
                //    {
                //        AutoCreateSqlTable = true,
                //        TableName = "SeriLogs"
                //    }
                //)
                //.WriteTo.File(
                //    "logs/log.log",
                //    rollingInterval: RollingInterval.Hour)
                .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // add Serilog as the logging provider
            services.AddLogging(b =>
            {
                b.AddSerilog(_serilogLogger);
            });

            services.AddCors();

            services.AddPersistence(Configuration);
            services.AddCore(Configuration);

            // prevents Mvc to throw 400 on invalid RequestBody
            // this is since we're using Fluent to do the same
            // within the Action Method
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddJwtBearerAuthentication();

            //services.AddResponseCaching();

            services.AddControllers();

            services.AddSwaggerWithVersioning();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
            {
                builder.AllowAnyHeader()
                .AllowAnyOrigin()
                .AllowAnyMethod();
            });

            app.UseSwaggerWithVersioning(provider);

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            //app.UseCaching();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
