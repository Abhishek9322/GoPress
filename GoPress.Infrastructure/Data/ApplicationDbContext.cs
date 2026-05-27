using GoPress.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Infrastructure.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options ) :base(options)
        {
            
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<CustomerProfile> CustomerProfiles { get; set; }
        public DbSet<DeliveryBoyProfile> DeliveryBoyProfiles { get; set; }
        public DbSet<ShopOwnerProfile> ShopOwnerProfiles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User -> CustomerProfile
            modelBuilder.Entity<CustomerProfile>()
                .HasOne(x => x.User)
                .WithOne(x => x.CustomerProfile)
                .HasForeignKey<CustomerProfile>(x => x.UserId);

            // User -> DeliveryBoyProfile
            modelBuilder.Entity<DeliveryBoyProfile>()
                .HasOne(x => x.User)
                .WithOne(x => x.DeliveryBoyProfile)
                .HasForeignKey<DeliveryBoyProfile>(x => x.UserId);

            // User -> ShopOwnerProfile
            modelBuilder.Entity<ShopOwnerProfile>()
                .HasOne(x => x.User)
                .WithOne(x => x.ShopOwnerProfile)
                .HasForeignKey<ShopOwnerProfile>(x => x.UserId);

            // User -> RefreshTokens
            modelBuilder.Entity<RefreshToken>()
                .HasOne(x => x.User)
                .WithMany(x => x.RefreshTokens)
                .HasForeignKey(x => x.UserId);
        }
    }
}
