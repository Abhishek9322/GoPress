using GoPress.Application.Comman.Caching;
using GoPress.Application.Features.Orders.ProcessingOrder.Command;
using GoPress.Application.Features.Orders.Responses;
using GoPress.Application.Interfaces.Caching;
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
    public class StartDeliveryCommandHandler : IRequestHandler<StartDeliveryCommand, Response<string>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICacheService _cacheService;
        public StartDeliveryCommandHandler(IOrderRepository orderRepository,ICacheService cacheService)
        {
            _orderRepository = orderRepository;
            _cacheService=cacheService;
        }
        public async Task<Response<string>> Handle(StartDeliveryCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);

            if (order == null)
            {
                return new Response<string>(
                    "Order Not Found");
            }

            if (order.DeliveryBoyId
                != request.DeliveryBoyId)
            {
                return new Response<string>(
                    "Unauthorized");
            }

            if (order.Status
                != OrderStatusEnum.ReadyForDelivery)
            {
                return new Response<string>(
                    "Order not ready");
            }
                
            order.Status = OrderStatusEnum.OutForDelivery;

            await _orderRepository.UpdateAsync(order);
            await _cacheService.RemoveAsync(CacheKeys.ShopOwnerDashboard);

            return new Response<string>(
                "Delivery Started");
        }
    }
}
