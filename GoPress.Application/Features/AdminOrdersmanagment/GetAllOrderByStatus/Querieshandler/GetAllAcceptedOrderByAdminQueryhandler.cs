using GoPress.Application.DTOs.Admin;
using GoPress.Application.Features.AdminOrdersmanagment.GetAllOrderByStatus.Queries;
using GoPress.Application.Features.Orders.Responses;
using GoPress.Application.Comman.Caching;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using GoPress.Application.Interfaces.Caching;
using GoPress.Application.Interfaces.Repositories;
using GoPress.Domain.Enums;
using GoPress.Application.DTOs.Orders;

namespace GoPress.Application.Features.AdminOrdersmanagment.GetAllOrderByStatus.Querieshandler
{
    public class GetAllAcceptedOrderByAdminQueryhandler : IRequestHandler<GetAllAcceptedOrderByAdminQuery, Response<List<AdminOrderDto>>>
    {
        private readonly ILogger<GetAllAcceptedOrderByAdminQueryhandler> _logger;
        private readonly ICacheService _cacheService;
        private readonly IOrderRepository _orderRepository;
        public GetAllAcceptedOrderByAdminQueryhandler(IOrderRepository orderRepository,
            ILogger<GetAllAcceptedOrderByAdminQueryhandler> logger,
            ICacheService cacheService)
        {
            _cacheService = cacheService;
            _logger = logger;
            _orderRepository = orderRepository;

        }
        public async Task<Response<List<AdminOrderDto>>> Handle(GetAllAcceptedOrderByAdminQuery request, CancellationToken cancellationToken)
        {
            var cachedOrder = await _cacheService.GetAsync<List<AdminOrderDto>>(CacheKeys.AcceptedOrders);

            if(cachedOrder != null)
            {
                _logger.LogInformation("Accepted Orders fetched from cache.");

                return new Response<List<AdminOrderDto>>(cachedOrder,
                    "Accepted Orders Retrieved Successfully (Cache)");
            }   

            _logger.LogInformation("Fetching Accepted Orders from database.");

            var orders = await _orderRepository.GetOrdersByStatusAsync(OrderStatusEnum.Accepted);

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

            await _cacheService.SetAsync(CacheKeys.AcceptedOrders, response, TimeSpan.FromMinutes(5));

            _logger.LogInformation("Accepted Orders cached successfully.");

            return new Response<List<AdminOrderDto>>(response, "Accepted Orders Retrieved Successfully");
        }
    }
}
