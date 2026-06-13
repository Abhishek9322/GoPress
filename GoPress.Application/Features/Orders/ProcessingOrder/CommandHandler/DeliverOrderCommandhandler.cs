using GoPress.Application.Features.Orders.ProcessingOrder.Command;
using GoPress.Application.Features.Orders.Responses;
using GoPress.Application.Interfaces.Repositories;
using GoPress.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Orders.ProcessingOrder.CommandHandler
{
    public class DeliverOrderCommandhandler : IRequestHandler<DeliverOrderCommand, Response<string>>
    {
        private readonly IOrderRepository _orderRepository;
        public DeliverOrderCommandhandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Response<string>> Handle(DeliverOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);

            if (order == null)
            {
                return new Response<string>(
                    "Order Not Found");
            }

            if (order.DeliveryBoyId != request.DeliveryBoyId)
            {
                return new Response<string>(
                    "You are not assigned to this order");
            }

            if (order.Status != OrderStatusEnum.OutForDelivery)
            {
                return new Response<string>(
                    "Order is not ready for delivery");
            }

            order.Status = OrderStatusEnum.Delivered;
            order.DeliveryDate = DateTime.UtcNow;
            await _orderRepository.UpdateAsync(order);

            return new Response<string>(
                "Order Delivered Successfully");

        }
    }
}
