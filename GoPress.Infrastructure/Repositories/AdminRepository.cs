using GoPress.Application.DTOs.Admin;
using GoPress.Application.Interfaces.Repositories;
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
    public class AdminRepository : IAdminRepository     
    {
        private readonly ApplicationDbContext _context; 
        public AdminRepository(ApplicationDbContext context)
        {
            _context= context;
        }
        public async Task<AdminDashboardDto> GetDashboardAsync()
        {
            return new AdminDashboardDto
            {
                TotalCustomers =
                     await _context.ApplicationUsers
                         .CountAsync(x => x.Role == UserRoleenum.Customer),


                TotalShopOwners =
                    await _context.ApplicationUsers
                        .CountAsync(x => x.Role == UserRoleenum.ShopOwner),

                TotalDeliveryBoys =
                    await _context.ApplicationUsers
                        .CountAsync(x => x.Role == UserRoleenum.DeliveryBoy),

                PendingShopOwnerApprovals =
                    await _context.ApplicationUsers
                        .CountAsync(x =>
                            x.Role == UserRoleenum.ShopOwner &&
                            !x.IsApproved),

                PendingDeliveryBoyApprovals =
                    await _context.ApplicationUsers
                        .CountAsync(x =>
                            x.Role == UserRoleenum.DeliveryBoy &&
                            !x.IsApproved),

                ActiveUsers =
                    await _context.ApplicationUsers
                        .CountAsync(x => x.IsActive),

                InactiveUsers =
                    await _context.ApplicationUsers
                        .CountAsync(x => !x.IsActive),


                TotalOrders =
                    await _context.Orders
                        .CountAsync(),

                PendingOrders =
                    await _context.Orders
                        .CountAsync(x =>
                            x.Status == OrderStatusEnum.Pending),

                CompletedOrders =
                    await _context.Orders
                        .CountAsync(x =>
                            x.Status == OrderStatusEnum.Delivered)

            };
        }
    }
}
