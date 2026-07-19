using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GoPress.Domain.Entities;
using GoPress.Application.Interfaces.Repositories;
using GoPress.Infrastructure.Data;
using System.Diagnostics;
using GoPress.Domain.Enums;


namespace GoPress.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task<bool> IsEmailExistsAsync(string email)
        {
           return _context.ApplicationUsers.AnyAsync(u => u.Email == email);
        }
        public async Task AddUserAsync(ApplicationUser user)
        {
            await _context.ApplicationUsers.AddAsync(user);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
        {
          return await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<ApplicationUser?> GetByIdAsync(int id)
        {
            return await _context.ApplicationUsers
           .FirstOrDefaultAsync(x => x.Id == id);
        }

        public  async Task<List<ApplicationUser>> GetPendingShopOwnersAsync()
        {
            return await _context.ApplicationUsers
                .Include(x=>x.ShopOwnerProfile)
                .Where(x => x.Role == UserRoleenum.ShopOwner && !x.IsApproved)
                .ToListAsync();
        }

        public async Task<List<ApplicationUser>> GetPendingDeliveryBoysAsync()
        {
            return await _context.ApplicationUsers
                .Include(x=>x.DeliveryBoyProfile)
                 .Where(x => x.Role == UserRoleenum.DeliveryBoy && !x.IsApproved)
                 .ToListAsync();
        }
        public async Task<List<ApplicationUser>> GetApprovedShopownerAsync()
        {
            return await _context.ApplicationUsers
                 .Include(x => x.ShopOwnerProfile)
                 .Where(x => x.Role == UserRoleenum.ShopOwner && x.IsApproved)
                 .ToListAsync();
        }

        public async Task<List<ApplicationUser>> GetApprovedDeliveryBoyAsync()
        {
            return await _context.ApplicationUsers
                .Include(x => x.DeliveryBoyProfile)
                .Where(x => x.Role == UserRoleenum.DeliveryBoy && x.IsApproved)
                .ToListAsync();
        }
        public async Task UpdateAsync(ApplicationUser user)
        {
           _context.ApplicationUsers.Update(user);

            await _context.SaveChangesAsync();
        }

        public async Task<List<ApplicationUser>> GetAvailableShopsAsync(string city)
        {
            var currentTime = TimeOnly.FromDateTime(DateTime.Now);

            var shops = await _context.ApplicationUsers
                .Include(x => x.ShopOwnerProfile)
                .Where(x =>
                    x.Role == UserRoleenum.ShopOwner &&
                    x.IsApproved &&
                    x.IsActive &&
                    x.ShopOwnerProfile != null &&
                    x.ShopOwnerProfile.City.Trim().ToLower() == city.Trim().ToLower() &&
                    x.ShopOwnerProfile.IsOpen)
                .OrderBy(x => x.ShopOwnerProfile.ShopName)
                .AsNoTracking()
                .ToListAsync();

            return shops.Where(x =>
                    currentTime >= x.ShopOwnerProfile!.OpeningTime &&
                    currentTime <= x.ShopOwnerProfile.ClosingTime)
                .ToList();
        }
        public Task<ShopOwnerProfile?> GetShopOwnerProfileByUserIdAsync(int userId)
        {
           return _context.ShopOwnerProfiles
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public Task UpdateShopStatus(ShopOwnerProfile shopOwnerProfile)
        {
           _context.ShopOwnerProfiles.Update(shopOwnerProfile);

            return _context.SaveChangesAsync();
        }

        public async Task<ApplicationUser?> GetCustomerWithProfileAsync(int customerId)
        {
            return await _context.ApplicationUsers
                 .Include(x => x.CustomerProfile)
                 .FirstOrDefaultAsync(x => x.Id == customerId);
        }
    }
}
