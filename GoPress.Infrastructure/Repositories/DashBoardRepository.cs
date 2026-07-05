using GoPress.Application.DTOs.Dashboard;
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
    public class DashBoardRepository:IDashBoardRepository
    {
        private readonly ApplicationDbContext _context;
        public DashBoardRepository(ApplicationDbContext context)
        {
            _context=context;
        }

        public async Task<ShopOwnerDashBoardDto> GetDashboardAsync(int shopOwnerId)
        {
            var orders = _context.Orders
                .Where(x => x.ShopOwnerId == shopOwnerId);

            return new ShopOwnerDashBoardDto
            {
                TotalOrders = await orders.CountAsync(),

                PendingOrders = await orders.CountAsync(x =>
                    x.Status == OrderStatusEnum.Pending),

                AcceptedOrders = await orders.CountAsync(x =>
                    x.Status == OrderStatusEnum.Accepted),

                PickedUpOrders = await orders.CountAsync(x =>
                    x.Status == OrderStatusEnum.PickedUp),

                DeliveredOrders = await orders.CountAsync(x =>
                    x.Status == OrderStatusEnum.Delivered),

                RejectedOrders = await orders.CountAsync(x =>
                    x.Status == OrderStatusEnum.Rejected),

                TotalRevenue = await orders
                  .Where(x => x.Status == OrderStatusEnum.Delivered)
                  .SumAsync(x => (decimal?)x.TotalAmount) ?? 0,

                TodayOrders = await orders.CountAsync(x =>
                    x.CreatedAt.Date == DateTime.UtcNow.Date),

                ThisMonthOrders = await orders.CountAsync(x =>
                    x.CreatedAt.Month == DateTime.UtcNow.Month &&
                    x.CreatedAt.Year == DateTime.UtcNow.Year)
            };
        }
    }
}
