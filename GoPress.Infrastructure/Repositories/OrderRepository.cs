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

        public async Task<List<Order>> GetDeliveryOrdersAsync(int deliveryBoyId)
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
           // _context.Orders.Update(order);

            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Order order)
        {
            _context.Orders.Remove(order);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Order>> GetAvailableOrdersAsync()
        {
            return await _context.Orders
                 .Include(o => o.Customer)
                 .Include(o => o.ShopOwner)
                 .ThenInclude(o => o.ShopOwnerProfile)
                 .Where(x =>x.Status == OrderStatusEnum.Accepted && x.DeliveryBoyId == null)
                 .ToListAsync();
        }
    }
}
