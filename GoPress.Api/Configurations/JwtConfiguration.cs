using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GoPress.Api.Configurations
{
    public static class JwtConfiguration
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(
               JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters =
                       new TokenValidationParameters
                       {
                           ValidateIssuer = true,
                           ValidateAudience = true,
                           ValidateLifetime = true,
                           ValidateIssuerSigningKey = true,

                           ValidIssuer =
                               configuration["Jwt:Issuer"],

                           ValidAudience =
                               configuration["Jwt:Audience"],

                           IssuerSigningKey =
                               new SymmetricSecurityKey(
                                   Encoding.UTF8.GetBytes(
                                       configuration["Jwt:Key"]!))
                       };

               
                 // Read JWT from Cookie
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token =
                                context.Request
                                    .Cookies["AuthToken"];

                            return Task.CompletedTask;
                        }
                    };
               });
            services.AddAuthorization();

            return services;
        }
    }
}
