using GoPress.Application.Interfaces.Repositories;
using GoPress.Domain.Entities;
using GoPress.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Infrastructure.Repositories
{
    public class ProfileRepository:IProfileRepository
    {
        private readonly ApplicationDbContext _context;
        public ProfileRepository(ApplicationDbContext context)
        {
            _context= context;
        }

        public async Task<ApplicationUser?> GetByIdAsync(int id)
        {
            return await _context.ApplicationUsers
          .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ApplicationUser?> GetCustomerProfileAsync(int userId)
        {
           return await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<ApplicationUser?> GetDeliveryBoyProfileAsync(int userId)
        {
            return await _context.ApplicationUsers
                  .Include(x => x.DeliveryBoyProfile)
                  .FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<ApplicationUser?> GetShopOwnerProfileAsync(int userId)
        {
            return await _context.ApplicationUsers
                .Include(x => x.ShopOwnerProfile)
                .FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task updateAsync(ApplicationUser user)
        {
            _context.ApplicationUsers.Update(user);

            await _context.SaveChangesAsync();
        }
    }
}
