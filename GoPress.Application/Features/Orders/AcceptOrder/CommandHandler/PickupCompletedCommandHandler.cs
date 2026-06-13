using GoPress.Application.Features.Orders.AcceptOrder.Command;
using GoPress.Application.Features.Orders.Responses;
using GoPress.Application.Interfaces.Repositories;
using GoPress.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Orders.AcceptOrder.CommandHandler
{
    public class PickupCompletedCommandHandler : IRequestHandler<PickupCompletedCommand, Response<string>>
    {
        private readonly IOrderRepository _orderRespository;
        public PickupCompletedCommandHandler(IOrderRepository orderRepository)
        {
            _orderRespository = orderRepository;
        }
        public async Task<Response<string>> Handle(PickupCompletedCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRespository.GetByIdAsync(request.OrderId);

            if(order==null)
            {
                return new Response<string>("Order Not Found ");
            }

            if(order.DeliveryBoyId!=request.DeliveryBoyId)
            {
                return new Response<string>("You are not assigned to this order");
            }

            if(order.Status!=OrderStatusEnum.PickupAssigned)
            {
                return new Response<string>("Pickup not assigned yet");
            }

            order.Status = OrderStatusEnum.PickedUp;

            await _orderRespository.UpdateAsync(order);

            return new Response<string>("Clothes Picked Up Successfully");



        }
    }
}
