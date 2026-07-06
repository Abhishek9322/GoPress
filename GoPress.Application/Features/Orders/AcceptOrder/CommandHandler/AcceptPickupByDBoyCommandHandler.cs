using GoPress.Application.Comman.Caching;
using GoPress.Application.Features.Orders.AcceptOrder.Comman;
using GoPress.Application.Features.Orders.Responses;
using GoPress.Application.Interfaces.Caching;
using GoPress.Application.Interfaces.Repositories;
using GoPress.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<AcceptPickupByDBoyCommandHandler> _logger;
        private readonly ICacheService _cacheService;
        public AcceptPickupByDBoyCommandHandler(IOrderRepository orderRepository,
            ILogger<AcceptPickupByDBoyCommandHandler> logger,
            ICacheService cacheService)
        {
            _orderRepository = orderRepository;
            _logger = logger;
            _cacheService = cacheService;
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
                    "Order already accepted by another delivery boy");
            }

            order.DeliveryBoyId = request.DeliveryBoyId;

            order.Status = OrderStatusEnum.PickupAssigned;

            _logger.LogInformation(
                    "DeliveryBoy {DeliveryBoyId} accepted order {OrderId}",
              request.DeliveryBoyId,
              request.OrderId);

            await _orderRepository.UpdateAsync(order);

            await _cacheService.RemoveAsync(CacheKeys.AdminDashboard);
            await _cacheService.RemoveAsync(CacheKeys.ShopOwnerDashboard);
            await _cacheService.RemoveAsync(CacheKeys.DeliveryBoyDashboard);

            _logger.LogInformation(
                 "DeliveryBoy {DeliveryBoyId} accepted order successfully {OrderId}",
               request.DeliveryBoyId,
               request.OrderId);

            return new Response<string>("Pickup Accepted Successfully");
        }
    }
}
