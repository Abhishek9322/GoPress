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
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ApplicationDbContext _context;
        public RefreshTokenRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(RefreshToken refreshToken)
        {
            await _context.RefreshTokens
                .AddAsync(refreshToken);

            await _context.SaveChangesAsync();
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            return await _context.RefreshTokens
               .Include(x => x.User)
               .FirstOrDefaultAsync(
                   x => x.Token == token);
        }

        public async Task UpdateAsync(RefreshToken refreshToken)
        {
           await _context.SaveChangesAsync();
        }
    }
}
