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
    public class ShopOwnerStartProcessingCommandHandler : IRequestHandler<ShopOwnerStartProcessingCommand, Response<string>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICacheService _cacheService;
        public ShopOwnerStartProcessingCommandHandler(IOrderRepository orderRepository,ICacheService cacheService)
        {
            _cacheService = cacheService;
            _orderRepository = orderRepository; 
        }
        public async Task<Response<string>> Handle(ShopOwnerStartProcessingCommand request, CancellationToken cancellationToken)
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

            if (order.Status != OrderStatusEnum.PickedUp)
            {
                return new Response<string>(
                    "Order must be Picked Up first");
            }

            order.Status =OrderStatusEnum.Processing;

            await _orderRepository.UpdateAsync(order);

            await _cacheService.RemoveAsync(CacheKeys.AdminDashboard);
            await _cacheService.RemoveAsync(CacheKeys.ShopOwnerDashboard);
            await _cacheService.RemoveAsync(CacheKeys.DeliveryBoyDashboard);
            await _cacheService.RemoveAsync(CacheKeys.CustomerDashboard);

            return new Response<string>("Order Processing Started");
        }
    }
}
