using GoPress.Application.DTOs.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Interfaces.Repositories
{
    public interface IDashBoardRepository
    {
        Task<ShopOwnerDashBoardDto> GetDashboardAsync(int shopOwnerId);
    }
}
