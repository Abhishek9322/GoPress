using GoPress.Application.Features.Orders.Commands;
using GoPress.Application.Features.Orders.Responses;
using GoPress.Application.Interfaces.Repositories;
using GoPress.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Orders.CommandHandler
{
    public class RejectOrderCommandHandler : IRequestHandler<RejectOrderCommand, Response<string>>
    {
        private readonly IOrderRepository _orderRepository;
        public RejectOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Response<string>> Handle(RejectOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);

            if (order == null)
            {
                return new Response<string>(
                   "Order Not Found");
            }

            if (order.ShopOwnerId != request.ShopOwnerId)
            {
                return new Response<string>(
                    "You cannot access this order");
            }

            if (order.Status != OrderStatusEnum.Pending)
            {
                return new Response<string>(
                    "Only pending orders can be rejected");
            }

            order.Status = OrderStatusEnum.Rejected;

            await _orderRepository.UpdateAsync(order);

            return new Response<string>(
                "Order Rejected Successfully");
        }
    }
}
