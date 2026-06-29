using GoPress.Application.Comman.Caching;
using GoPress.Application.Features.Orders.CreateOrder.Command;
using GoPress.Application.Features.Orders.Responses;
using GoPress.Application.Interfaces.Caching;
using GoPress.Application.Interfaces.Repositories;
using GoPress.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Orders.CreateOrder.CommaandHandlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<int>>
    {
        private readonly IShopOwnerClothPriceRepository _shopOwnerClothPriceRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<CreateOrderCommandHandler> _logger;
        private readonly ICacheService _cacheService;
        public CreateOrderCommandHandler(IOrderRepository orderRepository,
            IShopOwnerClothPriceRepository shopOwnerClothPriceRepository,
            ILogger<CreateOrderCommandHandler> logger,
            ICacheService cacheService)
        {
            _orderRepository = orderRepository;
            _shopOwnerClothPriceRepository = shopOwnerClothPriceRepository;
            _logger = logger;
            _cacheService = cacheService;
        }
        public async Task<Response<int>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            decimal totalAmount = 0;
            var orderItems = new List<OrderItem>();

            foreach (var item in request.Order.OrderItems)
            {
                var clothPrice = await _shopOwnerClothPriceRepository
                         .GetByShopOwnerAndClothTypeAsync(request.Order.ShopOwnerId, item.ClothTypeId);

                if (clothPrice == null)
                {
                    return new Response<int>(
                        "Price not configured");
                }

                var totalPrice = clothPrice.Price * item.Quantity;

                totalAmount += totalPrice;

                orderItems.Add(new OrderItem
                {
                    ClothTypeId = item.ClothTypeId,
                    ClothName = clothPrice.ClothType.Name,
                    Quantity = item.Quantity,
                    Price = clothPrice.Price,
                    TotalPrice = totalPrice
                });
            }
            var order = new Order
            {
                CustomerId = request.CustomerId,

                ShopOwnerId = request.Order.ShopOwnerId,

                PickupAddress = request.Order.PickupAddress,

                DeliveryAddress = request.Order.DeliveryAddress,

                PickupDate = request.Order.PickupDate,

                Notes = request.Order.Notes,

                TotalAmount = totalAmount,

                OrderItems = orderItems
            };

            _logger.LogInformation("Customer {CustomerId} is creating an order",
                request.CustomerId);

            var createOrder = await _orderRepository.CreateAsync(order);

            await _cacheService.RemoveAsync(CacheKeys.AdminDashboard);

            _logger.LogInformation("Order {OrderId} created successfully for customer {CustomerId}",
                  createOrder.Id,
                 request.CustomerId);

            return new Response<int>(
                createOrder.Id,
                "Order Created Successfully");
        }
    }
    
}
