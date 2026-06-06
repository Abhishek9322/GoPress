using GoPress.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> CreateAsync(Order order);

        Task<Order?> GetByIdAsync(int id);

        Task<List<Order>> GetCustomerOrdersAsync(int customerId);

        Task<List<Order>> GetShopOrdersAsync(int shopOwnerId);

        Task<List<Order>> GetDeliveryOrdersAsync(int deliveryBoyId);

        Task<List<Order>> GetAllOrdersAsync();

        Task UpdateAsync(Order order);
        Task DeleteAsync(Order order);
    }
}
