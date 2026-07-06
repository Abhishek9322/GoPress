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

        public async Task<ShopOwnerDashBoardDto> GetDashboardForShopOwnerAsync(int shopOwnerId)
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

        public async Task<DeliveryBoyDashBoardDto> GetDashboardForDeliveryBoyAsync(int deliveryBoyId)
        {
            var today = DateTime.UtcNow.Date;

            var firstDayOfMonth = new DateTime(
                today.Year,
                today.Month,
                1);

            var dashboard = new DeliveryBoyDashBoardDto
            {
                AvailableOrders = await _context.Orders.CountAsync(x =>
                                x.DeliveryBoyId == null &&
                                x.Status == OrderStatusEnum.Accepted),

                AcceptedOrders = await _context.Orders.CountAsync(x =>
                                x.DeliveryBoyId == deliveryBoyId &&
                                x.Status == OrderStatusEnum.Accepted),

                PickedUpOrders = await _context.Orders.CountAsync(x =>
                                x.DeliveryBoyId == deliveryBoyId &&
                                x.Status == OrderStatusEnum.PickedUp),

                ReadyForDeliveryOrders= await _context.Orders.CountAsync(x=>
                                x.DeliveryBoyId == deliveryBoyId &&
                                x.Status == OrderStatusEnum.ReadyForDelivery),

                OutForDeliveryOrders = await _context.Orders.CountAsync(x =>
                                x.DeliveryBoyId == deliveryBoyId &&
                                x.Status == OrderStatusEnum.OutForDelivery),

                DeliveredOrders = await _context.Orders.CountAsync(x =>
                               x.DeliveryBoyId == deliveryBoyId &&
                               x.Status == OrderStatusEnum.Delivered),

                TodayDeliveries = await _context.Orders.CountAsync(x =>
                               x.DeliveryBoyId == deliveryBoyId &&
                               x.Status == OrderStatusEnum.Delivered &&
                               x.DeliveryDate.HasValue &&
                               x.DeliveryDate.Value.Date == today),

                ThisMonthDeliveries = await _context.Orders.CountAsync(x =>
                               x.DeliveryBoyId == deliveryBoyId &&
                               x.Status == OrderStatusEnum.Delivered &&
                               x.DeliveryDate.HasValue &&
                               x.DeliveryDate.Value >= firstDayOfMonth)

            };
            return dashboard;
        }
    }
}
