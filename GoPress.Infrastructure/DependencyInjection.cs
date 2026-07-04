using GoPress.Infrastructure.Data;
using GoPress.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GoPress.Application.Interfaces.Repositories;
using GoPress.Application.Interfaces.Services;
using GoPress.Infrastructure.Services;
using GoPress.Application.Interfaces.Caching;
using GoPress.Infrastructure.Caching;

namespace GoPress.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this
            IServiceCollection service,
            IConfiguration configuration)
        {
            //datbse 
            service
                .AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("StartConnection"));
            });


            // Repository and interface and the service inject here 
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<IPasswordHasher, PasswordHasher>();
            service.AddScoped<IJwtService,JwtService>();
            service.AddScoped<IOrderRepository, OrderRepository>();
            service.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            service.AddScoped<IShopOwnerClothPriceRepository, ShopOwnerClothPriceRepository>();
            service.AddScoped<IAdminRepository, AdminRepository>();
            service.AddScoped<IProfileRepository, ProfileRepository>();

            service.AddSingleton<ICacheService, CacheService>();
            service.AddMemoryCache();

            return service;
        }

    }
}
