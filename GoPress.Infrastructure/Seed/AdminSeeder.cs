using GoPress.Domain.Entities;
using GoPress.Domain.Enums;
using GoPress.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Infrastructure.Seed
{
    public static class AdminSeeder
    {
        public static async Task SeedAdminAsync(ApplicationDbContext context)
        {
            // Check if Admin already exists
            var adminExists = await context
                .ApplicationUsers
                .AnyAsync(x => x.Role == UserRoleenum.Admin);

            if (adminExists)
            {
                return;
            }
            // Create Default Admin
            var admin = new ApplicationUser
            {
                FullName = "Super Admin",

                Email = "pran@gopress.com",

                PhoneNumber = "9322395458",

                // BCrypt Password
                PasswordHash =
                    BCrypt.Net.BCrypt.HashPassword(
                        "pran@123"),

                Role = UserRoleenum.Admin,

                IsApproved = true,

                IsActive = true
            };

            await context.ApplicationUsers
               .AddAsync(admin);

            await context.SaveChangesAsync();
        }

    }
}
