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
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Order> CreateAsync(Order order)
        {
            await _context.Orders.AddAsync(order);

            await _context.SaveChangesAsync();   

            return order;
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                 .Include(x => x.OrderItems)
                 .ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.Orders
                .Include(x => x.Customer)
                .Include(x => x.ShopOwner)
                .Include(x => x.OrderItems)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Order>> GetCustomerOrdersAsync(int customerId)
        {
            return await _context.Orders
                .Include(x => x.OrderItems)
                .Where(x => x.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<List<Order>> GetDeliveryOrdersAsync(int deliveryBoyId)   //Get Avlabal order All not selected by any other devlivry boy 
        {
            return await _context.Orders
                .Include(x => x.OrderItems)
                .Where(x => x.DeliveryBoyId == deliveryBoyId)
                .ToListAsync();
        }

        public async Task<List<Order>> GetShopOrdersAsync(int shopOwnerId)
        {
            return await _context.Orders
               .Include(x => x.OrderItems)
               .Where(x => x.ShopOwnerId == shopOwnerId)
               .ToListAsync();
        }

        public async Task UpdateAsync(Order order)
        {
           _context.Orders.Update(order);

            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Order order)
        {
            _context.Orders.Remove(order);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Order>> GetAvailableOrdersAsync()   //
        {
            return await _context.Orders
                 .Include(o => o.Customer)
                 .Include(o => o.ShopOwner)
                 .ThenInclude(o => o.ShopOwnerProfile)
                 .Where(x =>x.Status == OrderStatusEnum.Accepted && x.DeliveryBoyId == null)
                 .ToListAsync();
        }

        public async Task<List<Order>> GetReadyForDeliveryOrdersAsync(int deliveryBoyId)
        {
           return await _context.Orders
                .Include(x=>x.Customer)
                .Where(x => x.Status == OrderStatusEnum.ReadyForDelivery && x.DeliveryBoyId == deliveryBoyId)
                .ToListAsync();
        }

        public async Task<List<Order>> GetDeliveredOrdersByDeliveryBoyAsync(int deliveryBoyId)
        {
            return await _context.Orders
         .Include(x => x.Customer)
         .Include(x => x.ShopOwner)
             .ThenInclude(x => x.ShopOwnerProfile)
         .Where(x =>
             x.DeliveryBoyId == deliveryBoyId &&
             x.Status == OrderStatusEnum.Delivered)
         .OrderByDescending(x => x.CreatedAt)
         .ToListAsync();
        }

        public async Task<List<Order>> GetCompletedOrdersByShopOwnerAsync(int shopOwnerId)
        {
                    return await _context.Orders
                                         .Include(x => x.Customer)
                                         .Where(x =>
                                                x.ShopOwnerId == shopOwnerId &&
                                                x.Status == OrderStatusEnum.Delivered)
                                         .ToListAsync();
        }

        public async Task<List<Order>> GetRejectedOrdersByShopOwnerAsync(int shopOwnerId)
        {
            return await _context.Orders
                                 .Include(x => x.Customer)
                                   .Where(x =>
                                   x.ShopOwnerId == shopOwnerId &&
                                      x.Status == OrderStatusEnum.Rejected)
                                 .ToListAsync();
        }

        public async Task<List<Order>> GetReadyForDeliveryByShopOwnerAsync(int shopOwnerId)
        {
            return await _context.Orders
                                 .Include(x => x.Customer)
                                .Where(x =>
                                x.ShopOwnerId == shopOwnerId &&
                                 x.Status == OrderStatusEnum.ReadyForDelivery)
                              .ToListAsync();
        }
    }
}
