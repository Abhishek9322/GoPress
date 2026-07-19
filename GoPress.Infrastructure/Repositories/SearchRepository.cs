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
    public class SearchRepository : ISearchRepository
    {
        private readonly ApplicationDbContext _context;
        public SearchRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<ApplicationUser>> SearchShopsAsync(string keyword)
        {
            keyword = keyword.Trim().ToLower();
            return await _context.ApplicationUsers
                .Include(x => x.ShopOwnerProfile)
                .Where(x =>
                    x.Role == UserRoleenum.ShopOwner &&
                    x.ShopOwnerProfile != null &&
                    (
                        x.ShopOwnerProfile.ShopName.ToLower().Contains(keyword)
                        ||
                        x.FullName.ToLower().Contains(keyword)
                    )
                )
                .OrderBy(x => x.ShopOwnerProfile.ShopName)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
