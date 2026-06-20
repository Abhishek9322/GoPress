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
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ClothType> ClothTypes { get; set; }

        public DbSet<ShopOwnerClothPrice> ShopOwnerClothPrices { get; set; }    

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

            // CUSTOMER -> ORDERS
            modelBuilder.Entity<Order>()
                .HasOne(x => x.Customer)
                .WithMany(x => x.CustomerOrders)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            // SHOP OWNER -> ORDERS
            modelBuilder.Entity<Order>()
                .HasOne(x => x.ShopOwner)
                .WithMany(x => x.ShopOwnerOrders)
                .HasForeignKey(x => x.ShopOwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            // DELIVERY BOY -> ORDERS
            modelBuilder.Entity<Order>()
                .HasOne(x => x.DeliveryBoy)
                .WithMany(x => x.DeliveryBoyOrders)
                .HasForeignKey(x => x.DeliveryBoyId)
                .OnDelete(DeleteBehavior.Restrict);

            // ORDER -> ORDER ITEMS
            modelBuilder.Entity<OrderItem>()
                .HasOne(x => x.Order)
                .WithMany(x => x.OrderItems)
                .HasForeignKey(x => x.OrderId);

            // ShopOwner -> Cloth Prices

            modelBuilder.Entity<ShopOwnerClothPrice>()
                .HasOne(x => x.ShopOwner)
                .WithMany(x => x.ShopOwnerClothPrices)
                .HasForeignKey(x => x.ShopOwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            // ClothType -> ShopOwner Prices

            modelBuilder.Entity<ShopOwnerClothPrice>()
                .HasOne(x => x.ClothType)
                .WithMany(x => x.ShopOwnerClothPrices)
                .HasForeignKey(x => x.ClothTypeId)
                .OnDelete(DeleteBehavior.Restrict);


            //to fixt isdeleted proper
            modelBuilder.Entity<ClothType>()
                .Property(x => x.IsDeleted)
                .HasDefaultValue(false);

            modelBuilder.Entity<ClothType>()
                .Property(x => x.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");


            // ==========================
            // INDEXES
            // ==========================

            // User login lookup
            modelBuilder.Entity<ApplicationUser>()
                .HasIndex(x => x.Email)
                .IsUnique();

            // Refresh token lookup
            modelBuilder.Entity<RefreshToken>()
                .HasIndex(x => x.Token)
                .IsUnique();

            // Orders by customer        
            modelBuilder.Entity<Order>()
                .HasIndex(x => x.CustomerId);

            // Orders by shop owner
            modelBuilder.Entity<Order>()
                .HasIndex(x => x.ShopOwnerId);

            // Orders by delivery boy
            modelBuilder.Entity<Order>()
                .HasIndex(x => x.DeliveryBoyId);

            // Order items by order
            modelBuilder.Entity<OrderItem>()
                .HasIndex(x => x.OrderId);

            // cloth type name 
            modelBuilder.Entity<ClothType>()
                .HasIndex(x => x.Name)
                .IsUnique();

            // shopowner and cloth type  /prevenht from /Shirt ₹10/ Shirt ₹20 / by same shop owner
            modelBuilder.Entity<ShopOwnerClothPrice>()
                .HasIndex(x => new
              {
                  x.ShopOwnerId,
                  x.ClothTypeId
             })
              .IsUnique();
        }
    }
}
