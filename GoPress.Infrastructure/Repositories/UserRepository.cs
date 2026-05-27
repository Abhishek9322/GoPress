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
    }
}
