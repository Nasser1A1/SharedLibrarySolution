

using eCommerce.SharedLib.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace eCommerce.SharedLib.DependencyIncjection
{
    public static class SharedServiceContainer
    {
        public static IServiceCollection AddSharedServices<TContext>
            (this IServiceCollection services, IConfiguration configuration,string fileName) where TContext : DbContext
        {
            // Add JWT authentication scheme
            services.AddJWTAuthenticationScheme(configuration);
            services.AddDbContext<TContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("eCommerceConnection"), 
                    npsqlOptions => npsqlOptions.EnableRetryOnFailure());
            });

            // config Serilog
            Log.Logger = new LoggerConfiguration().
               MinimumLevel.Information().
               WriteTo.Debug().
               WriteTo.Console().
               WriteTo.File(
                path: $"{fileName}-.text",
                restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
                outputTemplate: "{Timestamp:yyyy-MM-DD HH:mm:ss.fff zzz} [{level:u3}] {message:lj}{NewLine}{Exception}", 
                rollingInterval: RollingInterval.Day).CreateLogger();
          

            return services;
        }

        public static IApplicationBuilder UseSharedPolicies(this IApplicationBuilder app)
        {
            // Global Exception Middleware
            app.UseMiddleware<GlobalException>();
            // Api Gateway Middleware Block outside requests
             app.UseMiddleware<ListenToOnlyApiGateway>();
            return app;
        }
    }
}
