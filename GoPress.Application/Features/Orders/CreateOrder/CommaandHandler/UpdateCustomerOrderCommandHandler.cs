using GoPress.Application.Features.Orders.CreateOrder.Command;
using GoPress.Application.Features.Orders.Responses;
using GoPress.Application.Interfaces.Repositories;
using GoPress.Domain.Entities;
using GoPress.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Orders.CreateOrder.CommaandHandlers
{
    public class UpdateCustomerOrderCommandHandler : IRequestHandler<UpdateCustomerOrderCommand, Response<string>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IShopOwnerClothPriceRepository _shopOwnerClothPriceRepository;
        private readonly ILogger<UpdateCustomerOrderCommandHandler> _logger;

        public UpdateCustomerOrderCommandHandler(
            IOrderRepository orderRepository,
            IShopOwnerClothPriceRepository shopOwnerClothPriceRepository,
            ILogger<UpdateCustomerOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository;
            _shopOwnerClothPriceRepository = shopOwnerClothPriceRepository;
            _logger = logger;
        }

        public async Task<Response<string>> Handle(UpdateCustomerOrderCommand request, CancellationToken cancellationToken)
        {
            var order =
                await _orderRepository.GetByIdAsync(request.OrderId);

            if (order == null)
            {
                return new Response<string>(
                    "Order Not Found");
            }
            // SECURITY CHECK

            if (order.CustomerId != request.CustomerId)
            {
                return new Response<string>(
                    "Unauthorized");
            }

            // ONLY PENDING ORDER CAN UPDATE

            if (order.Status != OrderStatusEnum.Pending)
            {
                return new Response<string>(
                    "Order Cannot Be Updated");
            }

            decimal totalAmount = 0;

            order.OrderItems.Clear();

            foreach (var item in request.Order.OrderItems)
            {
                var clothPrice = await _shopOwnerClothPriceRepository
                    .GetByShopOwnerAndClothTypeAsync(order.ShopOwnerId, item.ClothTypeId);

                if (clothPrice == null)
                {
                    return new Response<string>(
                        "Price Not Configured");
                }

                var totalPrice = clothPrice.Price * item.Quantity;

                totalAmount += totalPrice;

                order.OrderItems.Add(new OrderItem
                {
                    ClothTypeId = item.ClothTypeId,
                    ClothName = clothPrice.ClothType.Name,
                    Quantity = item.Quantity,
                    Price = clothPrice.Price,
                    TotalPrice = totalPrice
                });
            }

            order.PickupAddress =request.Order.PickupAddress;
            order.DeliveryAddress =request.Order.DeliveryAddress;
            order.PickupDate =request.Order.PickupDate;
            order.Notes =request.Order.Notes;
            order.TotalAmount = totalAmount;
            order.UpdatedAt = DateTime.UtcNow;

            _logger.LogInformation(
                "Customer {CustomerId} updating order {OrderId}",
              request.CustomerId,
              request.OrderId);

            await _orderRepository.UpdateAsync(order);

            _logger.LogInformation(
               "Order {OrderId} updated successfully",
              request.OrderId);

            return new Response<string>(
                "Order Updated Successfully");
        }
    }
    
}
