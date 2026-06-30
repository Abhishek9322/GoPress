using GoPress.Application.Comman.Caching;
using GoPress.Application.DTOs.Admin;
using GoPress.Application.DTOs.Orders;
using GoPress.Application.Features.AdminOrdersmanagment.GetAllOrderByStatus.Queries;
using GoPress.Application.Features.Orders.Responses;
using GoPress.Application.Interfaces.Caching;
using GoPress.Application.Interfaces.Repositories;
using GoPress.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.AdminOrdersmanagment.GetAllOrderByStatus.Querieshandler
{
    public class GetPendingOrdersByAdminQueryhandler : IRequestHandler<GetPendingOrdersByAdminQuery, Response<List<AdminOrderDto>>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICacheService _cacheService;
        private readonly ILogger<GetPendingOrdersByAdminQueryhandler> _logger;
        public GetPendingOrdersByAdminQueryhandler(IOrderRepository orderRepository,
            ICacheService cacheService,
            ILogger<GetPendingOrdersByAdminQueryhandler> logger)
        {
            _cacheService = cacheService;
            _orderRepository = orderRepository;
            _logger = logger;
        }
        public async Task<Response<List<AdminOrderDto>>> Handle(GetPendingOrdersByAdminQuery request, CancellationToken cancellationToken)
        {

          var cachedOrders= await _cacheService.GetAsync<List<AdminOrderDto>>(CacheKeys.PendingOrders);

            if (cachedOrders != null)
            {
                _logger.LogInformation("Pending Orders fetched from cache.");

                return new Response<List<AdminOrderDto>>(
                    cachedOrders,
                    "Pending Orders Retrieved Successfully (Cache)");
            }

            _logger.LogInformation("Fetching pending orders from database.");

            var orders = await _orderRepository.GetOrdersByStatusAsync(OrderStatusEnum.Pending);

            var response=orders.Select(order=>new AdminOrderDto
            {
                OrderId = order.Id,
                CustomerName = order.Customer.FullName,
                ShopOwnerName = order.ShopOwner.FullName,
                DeliveryBoyName = order.DeliveryBoy?.FullName,
                PickupAddress = order.PickupAddress,
                DeliveryAddress = order.DeliveryAddress,
                PickupDate = order.PickupDate,
                DeliveryDate = order.DeliveryDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                OrderItems = order.OrderItems.Select(item => new OrderItemResponseDto
                {
                    Id = item.Id,
                    ClothName = item.ClothName,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    TotalPrice = item.TotalPrice
                }).ToList()
            }).ToList();

            await _cacheService.SetAsync(CacheKeys.PendingOrders, response, TimeSpan.FromMinutes(5));

            _logger.LogInformation(
                "Pending Orders cached successfully.");

            return new Response<List<AdminOrderDto>>(
                response,
                "Pending Orders Retrieved Successfully");

        }
    }
}
