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
    public class GetAllCancaledOrderByAdminQueryHandler : IRequestHandler<GetAllCancaledOrderByAdminQuery, Response<List<AdminOrderDto>>>
    {
        private readonly ILogger<GetAllCancaledOrderByAdminQueryHandler> _logger;
        private readonly IOrderRepository _orderRepository;
        private readonly ICacheService _cacheService;
        public GetAllCancaledOrderByAdminQueryHandler(ILogger<GetAllCancaledOrderByAdminQueryHandler> logger,
            ICacheService cacheService,
            IOrderRepository orderRepository)
        {
            _cacheService = cacheService;
            _orderRepository = orderRepository;
            _logger = logger;
        }
        public async Task<Response<List<AdminOrderDto>>> Handle(GetAllCancaledOrderByAdminQuery request, CancellationToken cancellationToken)
        {
            var cachedOrder = await _cacheService.GetAsync<List<AdminOrderDto>>(CacheKeys.CancelledOrders);

            if(cachedOrder != null)
            {
                _logger.LogInformation("Cancelled Orders fetched from cache.");

                return new Response<List<AdminOrderDto>>(cachedOrder,
                    "Cancelled Orders Retrieved Successfully (Cache)");
            }

            _logger.LogInformation("Fetching Cancelled Orders from database.");

            var orders = await _orderRepository.GetOrdersByStatusAsync(OrderStatusEnum.Cancelled);

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
        
            _logger.LogInformation("Caching Cancelled Orders for future requests.");

            await _cacheService.SetAsync(CacheKeys.CancelledOrders, response, TimeSpan.FromMinutes(10));

            return new Response<List<AdminOrderDto>>(response,
                "Cancelled Orders Retrieved Successfully");
        }
    }
}
