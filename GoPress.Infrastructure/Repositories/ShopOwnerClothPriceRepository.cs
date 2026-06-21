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
    public class ShopOwnerClothPriceRepository : IShopOwnerClothPriceRepository
    {
        private readonly ApplicationDbContext _context;
        public ShopOwnerClothPriceRepository(ApplicationDbContext context)
        {
            _context=context;
        }
        public async Task AddAsync(ShopOwnerClothPrice clothPrice)
        {
            await _context.ShopOwnerClothPrices
                .AddAsync(clothPrice);

            await _context.SaveChangesAsync();
        }

      
        public async Task<ShopOwnerClothPrice?> GetByShopOwnerAndClothTypeAsync(int shopOwnerId, int clothTypeId)
        {
            return await _context.ShopOwnerClothPrices
                  .Include(x => x.ClothType)
                  .FirstOrDefaultAsync(x =>
                   x.ShopOwnerId == shopOwnerId &&
                   x.ClothTypeId == clothTypeId);
        }

        public async Task<List<ShopOwnerClothPrice>> GetShopPricesAsync(int shopOwnerId)
        {
            return await _context.ShopOwnerClothPrices
                 .Include(x => x.ClothType)
                 .Where(x => x.ShopOwnerId == shopOwnerId)
                 .ToListAsync();
        }
        public async Task<List<ShopOwnerClothPrice>> GetByShopOwnerIdAsync(int shopOwnerId)
        {
            return await _context.ShopOwnerClothPrices
                 .Include(c => c.ClothType)
                 .Where(x => x.ShopOwnerId == shopOwnerId)
                 .ToListAsync();
        }
        public async Task<ShopOwnerClothPrice?> GetByIdAsync(int id)
        {
            return await _context.ShopOwnerClothPrices.FirstOrDefaultAsync(c=>c.Id == id);

        }
        public async Task UpdateAsync(ShopOwnerClothPrice clothPrice)
        {
            _context.ShopOwnerClothPrices
                .Update(clothPrice);

            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(ShopOwnerClothPrice clothPrice)
        {
            _context.ShopOwnerClothPrices
               .Remove(clothPrice);

            await _context.SaveChangesAsync();
        }

       
    }
}
