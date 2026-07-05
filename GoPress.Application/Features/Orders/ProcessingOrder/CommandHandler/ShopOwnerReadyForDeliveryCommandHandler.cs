using GoPress.Application.Comman.Caching;
using GoPress.Application.Features.Orders.ProcessingOrder.Command;
using GoPress.Application.Features.Orders.Responses;
using GoPress.Application.Interfaces.Caching;
using GoPress.Application.Interfaces.Repositories;
using GoPress.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<ShopOwnerReadyForDeliveryCommandHandler> _logger;
        private readonly ICacheService _cacheService;
        public ShopOwnerReadyForDeliveryCommandHandler(IOrderRepository orderRepository,
            ILogger<ShopOwnerReadyForDeliveryCommandHandler> logger,
            ICacheService cacheService)
        {
            _orderRepository = orderRepository;
            _cacheService= cacheService;
            _logger = logger;
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

            _logger.LogInformation(
                    "Order {OrderId} marked ReadyForDelivery by ShopOwner {ShopOwnerId}",
                 request.OrderId,
                request.ShopOwnerId);

            await _orderRepository.UpdateAsync(order);
            await _cacheService.RemoveAsync(CacheKeys.ShopOwnerDashboard);

            _logger.LogInformation(
                "Order {OrderId} updated to ReadyForDelivery successfully",
                request.OrderId);

            return new Response<string>(
                "Order Ready For Delivery");
        }
    }
}
