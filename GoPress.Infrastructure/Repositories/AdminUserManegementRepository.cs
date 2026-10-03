using GoPress.Application.DTOs.Admin.UserManegment;
using GoPress.Application.Interfaces.Repositories;
using GoPress.Domain.Entities;
using GoPress.Domain.Enums;
using GoPress.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Infrastructure.Repositories
{
    public class AdminUserManegementRepository:IAdminUserManegementRepository
    {
        private readonly ApplicationDbContext _context;
        public AdminUserManegementRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ApplicationUser>> GetAllCustomerDetails(int userId)
        {
            return await _context.ApplicationUsers
                 .Include(x => x.CustomerProfile)
                 .Where(x => x.Role == UserRoleenum.Customer && x.Id == userId)
                 .AsNoTracking()
                 .ToListAsync();
        }

        public async Task<List<ApplicationUser>> GetAllCustomerProfile()
        {
            return await _context.ApplicationUsers
                  .Include(x => x.CustomerProfile)
                  .Where(x=>x.Role == UserRoleenum.Customer)
                  .AsNoTracking()
                  .ToListAsync();
        }
    }
}
