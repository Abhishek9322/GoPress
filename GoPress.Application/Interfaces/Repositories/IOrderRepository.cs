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
        Task<List<Order>> GetReadyForDeliveryOrdersAsync(int deliveryBoyId);
        Task<List<Order>> GetAvailableOrdersAsync();
        Task<List<Order>> GetDeliveredOrdersByDeliveryBoyAsync(int deliveryBoyId);

        Task<List<Order>> GetCompletedOrdersByShopOwnerAsync(int shopOwnerId);

        Task<List<Order>> GetRejectedOrdersByShopOwnerAsync( int shopOwnerId);

        Task<List<Order>> GetReadyForDeliveryByShopOwnerAsync(int shopOwnerId);

        Task UpdateAsync(Order order);
        Task DeleteAsync(Order order);
    }
}
