using GoPress.Application.Features.Orders.AcceptOrder.Comman;
using GoPress.Application.Features.Orders.Responses;
using GoPress.Application.Interfaces.Repositories;
using GoPress.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Orders.AcceptOrder.CommanHand
{
    public class AcceptPickupByDBoyCommandHandler : IRequestHandler<AcceptPickupByDBoyCommand, Response<string>>
    {
        private readonly IOrderRepository _orderRepository;
        public AcceptPickupByDBoyCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Response<string>> Handle(AcceptPickupByDBoyCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);

            if(order==null)
            {
                return new Response<string>("Order Not Found ");
            }

            if (order.Status != OrderStatusEnum.Accepted)
            {
                return new Response<string>(
                    "Order is not available for pickup");
            }

            if (order.DeliveryBoyId != null)
            {
                return new Response<string>(
                    "Order already assigned");
            }

            order.DeliveryBoyId = request.DeliveryBoyId;

            order.Status = OrderStatusEnum.Accepted;

            return new Response<string>("Pickup Accepted Successfully");
        }
    }
}
