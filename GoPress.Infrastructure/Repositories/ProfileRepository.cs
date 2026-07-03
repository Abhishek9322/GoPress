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

        public async Task<ApplicationUser?> GetCustomerProfileAsync(int userId)
        {
           return await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == userId);
        }
    }
}
