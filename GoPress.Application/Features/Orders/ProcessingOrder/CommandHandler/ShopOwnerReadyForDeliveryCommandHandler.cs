using GoPress.Application.Features.Orders.ProcessingOrder.Command;
using GoPress.Application.Features.Orders.Responses;
using GoPress.Application.Interfaces.Repositories;
using GoPress.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Orders.ProcessingOrder.CommandHandler
{
    public class ShopOwnerReadyForDeliveryCommandHandler : IRequestHandler<ShopOwnerReadyForDeliveryCommand, Response<string>>
    {
        private readonly IOrderRepository _orderRepository;
        public ShopOwnerReadyForDeliveryCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Response<string>> Handle(ShopOwnerReadyForDeliveryCommand request, CancellationToken cancellationToken)
        {
            var order =
                  await _orderRepository.GetByIdAsync(
                      request.OrderId);

            if (order == null)
            {
                return new Response<string>(
                    "Order Not Found");
            }

            if (order.ShopOwnerId != request.ShopOwnerId)
            {
                return new Response<string>(
                    "You cannot manage this order");
            }

            if (order.Status != OrderStatusEnum.Processing)
            {
                return new Response<string>(
                    "Order is not in Processing state");
            }

            order.Status =OrderStatusEnum.ReadyForDelivery;

            await _orderRepository.UpdateAsync(order);

            return new Response<string>(
                "Order Ready For Delivery");
        }
    }
}
