using GoPress.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Interfaces.Repositories
{
    public interface IShopOwnerClothPriceRepository
    {
        Task AddAsync(ShopOwnerClothPrice clothPrice);
        Task<ShopOwnerClothPrice?> GetByShopOwnerAndClothTypeAsync(int shopOwnerId,int clothTypeId);
        Task<List<ShopOwnerClothPrice>> GetShopPricesAsync(int shopOwnerId);
        Task UpdateAsync(ShopOwnerClothPrice clothPrice);
        Task DeleteAsync(ShopOwnerClothPrice clothPrice);


    }
}
