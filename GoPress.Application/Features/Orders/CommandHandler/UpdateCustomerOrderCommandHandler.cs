using GoPress.Application.Features.Orders.Commands;
using GoPress.Application.Features.Orders.Responses;
using GoPress.Application.Interfaces.Repositories;
using GoPress.Domain.Entities;
using GoPress.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Orders.CommandHandler
{
    public class UpdateCustomerOrderCommandHandler : IRequestHandler<UpdateCustomerOrderCommand, Response<string>>
    {
        private readonly IOrderRepository _orderRepository;

        public UpdateCustomerOrderCommandHandler(
            IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
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

            // UPDATE BASIC DETAILS

            order.PickupAddress = request.Order.PickupAddress;
            order.DeliveryAddress =request.Order.DeliveryAddress;
            order.PickupDate =request.Order.PickupDate;
            order.Notes = request.Order.Notes;

            // REMOVE OLD ITEMS

            order.OrderItems.Clear();

            decimal totalAmount = 0;

            foreach (var item in request.Order.OrderItems)
            {
                decimal totalPrice =
                    item.Quantity * item.Price;

                totalAmount += totalPrice;

                order.OrderItems.Add(new OrderItem
                {
                    ClothName = item.ClothName,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    TotalPrice = totalPrice
                });
            }

            order.TotalAmount = totalAmount;

            order.UpdatedAt = DateTime.UtcNow;

            await _orderRepository.UpdateAsync(order);

            return new Response<string>(
                "Order Updated Successfully");
        }
    }
    
}
