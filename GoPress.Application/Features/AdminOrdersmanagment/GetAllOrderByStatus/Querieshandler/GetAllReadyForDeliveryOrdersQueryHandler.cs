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
    public class GetAllReadyForDeliveryOrdersQueryHandler : IRequestHandler<GetAllReadyForDeliveryOrdersQuery, Response<List<AdminOrderDto>>>
    {
        private readonly ILogger<GetdeliverdOrderByAdminQueryHandler> _logger;
        private readonly ICacheService _cacheService;
        private readonly IOrderRepository _orderRepository;
        public GetAllReadyForDeliveryOrdersQueryHandler(ILogger<GetdeliverdOrderByAdminQueryHandler> logger,
            ICacheService cacheService,
            IOrderRepository orderRepository)
        {
            _cacheService = cacheService;
            _logger = logger;
            _orderRepository = orderRepository;
        }
        public async Task<Response<List<AdminOrderDto>>> Handle(GetAllReadyForDeliveryOrdersQuery request, CancellationToken cancellationToken)
        {
            var cachedOrder = await _cacheService.GetAsync<List<AdminOrderDto>>(CacheKeys.ReadyForDelivery);

            if (cachedOrder != null)
            {
                _logger.LogInformation("ReadyForDelivery Order Fetched From cache .");
                return new Response<List<AdminOrderDto>>(cachedOrder, "Orders Retrieved Successfully (Cache)");
            }

            _logger.LogInformation("ReadyForDelivery Order Fetcheing From database");

            var orders = await _orderRepository.GetOrdersByStatusAsync(OrderStatusEnum.ReadyForDelivery);

            
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

            await _cacheService.SetAsync(CacheKeys.ReadyForDelivery, response, TimeSpan.FromMinutes(5));

            _logger.LogInformation("ReadyForDelivery Orders fetched from database and cached.");

            return new Response<List<AdminOrderDto>>(response, "Orders Retrieved Successfully");


        }

    }
}
