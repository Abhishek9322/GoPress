using GoPress.Application.DTOs.Admin;
using GoPress.Application.Features.AdminOrdersmanagment.GetAllOrderByStatus.Queries;
using GoPress.Application.Features.Orders.Responses;
using GoPress.Application.Interfaces.Caching;
using GoPress.Application.Comman.Caching;
using GoPress.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoPress.Domain.Enums;
using GoPress.Application.DTOs.Orders;

namespace GoPress.Application.Features.AdminOrdersmanagment.GetAllOrderByStatus.Querieshandler
{
    public class GetAllRejectedOrderByAdminQueryHandler : IRequestHandler<GetAllRejectedOrderByAdminQuery, Response<List<AdminOrderDto>>>
    {
        private readonly ILogger<GetAllRejectedOrderByAdminQueryHandler> _logger;
        private readonly ICacheService _cacheService;
        private readonly IOrderRepository _orderRepository;
        public GetAllRejectedOrderByAdminQueryHandler(ILogger<GetAllRejectedOrderByAdminQueryHandler> logger,
            ICacheService  cacheService,
            IOrderRepository orderRepository)
        {
            _cacheService = cacheService;
            _orderRepository = orderRepository;
            _logger = logger;
        }
        public async Task<Response<List<AdminOrderDto>>> Handle(GetAllRejectedOrderByAdminQuery request, CancellationToken cancellationToken)
        {
            var cachedOrder = await _cacheService.GetAsync<List<AdminOrderDto>>(CacheKeys.RejectedOrders);

            if (cachedOrder != null)
            {
                _logger.LogInformation("Rejected Orders fetched from cache.");

                return new Response<List<AdminOrderDto>>(cachedOrder, 
                    "Rejected Orders Retrieved Successfully (Cache)");
            }

            _logger.LogInformation("Rejected Order Fetchign From Database .");

            var orders = await _orderRepository.GetOrdersByStatusAsync(OrderStatusEnum.Rejected);

            var response = orders.Select(order => new AdminOrderDto
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

            await _cacheService.SetAsync(CacheKeys.RejectedOrders, response, TimeSpan.FromMinutes(5));

            _logger.LogInformation("Rejected Orders cached successfully.");

            return new Response<List<AdminOrderDto>>(response, "Rejected Orders Retrieved Successfully");
        }
    }
}
