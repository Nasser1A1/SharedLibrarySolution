using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.SharedLib.DependencyIncjection
{
    public static class JWTAuthenticationScheme
    {

        public static IServiceCollection AddJWTAuthenticationScheme(this IServiceCollection services, IConfiguration configuration)
        {
            // Retrieve JWT settings from appsettings.json
            var jwtSettings = configuration.GetSection("Authentication");
            var key = Encoding.ASCII.GetBytes(jwtSettings["Key"] ?? throw new InvalidOperationException("JWT Key not configured in appsettings.json."));
            var issuer = configuration["Authentication:Issuer"] ?? throw new InvalidOperationException("JWT Issuer not configured in appsettings.json.");
            var audience = configuration["Authentication:Audience"] ?? throw new InvalidOperationException("JWT Audience not configured in appsettings.json.");

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };

                    // Optional debug logging:
                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = ctx =>
                        {
                            Console.WriteLine("Auth failed: " + ctx.Exception.Message);
                            return Task.CompletedTask;
                        }
                    };
                });
            return services;
        }
    }


}

