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
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Orders.ProcessingOrder.CommandHandler
{
    public class DeliverOrderCommandhandler : IRequestHandler<DeliverOrderCommand, Response<string>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<DeliverOrderCommandhandler> _logger;
        private readonly ICacheService _cacheService;
        public DeliverOrderCommandhandler(IOrderRepository orderRepository,
            ILogger<DeliverOrderCommandhandler> logger,
            ICacheService cacheService)
        {
            _logger = logger;
            _cacheService = cacheService;
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

            _logger.LogInformation(
                    "Order {OrderId} delivered by DeliveryBoy {DeliveryBoyId}",
               request.OrderId,
               request.DeliveryBoyId);

            await _orderRepository.UpdateAsync(order);
            await _cacheService.RemoveAsync(CacheKeys.AdminDashboard);
            await _cacheService.RemoveAsync(CacheKeys.ShopOwnerDashboard);
            await _cacheService.RemoveAsync(CacheKeys.DeliveryBoyDashboard);

            _logger.LogInformation(
                "Order {OrderId} updated to Delivered successfully",
                request.OrderId);

            return new Response<string>(
                "Order Delivered Successfully");

        }
    }
}
