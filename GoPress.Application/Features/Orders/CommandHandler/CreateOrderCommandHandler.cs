using GoPress.Application.Features.Orders.Commands;
using GoPress.Application.Features.Orders.Responses;
using GoPress.Application.Interfaces.Repositories;
using GoPress.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Orders.CommandHandler
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<int>>
    {
        private readonly IOrderRepository _orderRepository;
        public CreateOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Response<int>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            decimal totalAmount = 0;

            var orderItems = new List<OrderItem>();

            foreach (var item in request.Order.OrderItems)
            {
                var totalPrice =
                    item.Quantity * item.Price;

                totalAmount += totalPrice;

                orderItems.Add(new OrderItem
                {
                    ClothName = item.ClothName,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    TotalPrice = totalPrice
                });
            }

            var order = new Order
            {
                CustomerId = request.CustomerId,
                ShopOwnerId = request.Order.ShopOwnerId,    //problem is here dot send this with the order 
                PickupAddress = request.Order.PickupAddress,
                DeliveryAddress = request.Order.DeliveryAddress,
                PickupDate = request.Order.PickupDate,
                Notes =request.Order.Notes,
                TotalAmount = totalAmount,
                OrderItems = orderItems
            };

            var createdOrder =
                await _orderRepository.CreateAsync(order);

            return new Response<int>(
                createdOrder.Id,
                "Order Created Successfully");
        }
    }
    
}
