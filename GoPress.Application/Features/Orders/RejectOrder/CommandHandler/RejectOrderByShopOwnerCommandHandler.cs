using GoPress.Application.Comman.Caching;
using GoPress.Application.Features.Orders.RejectOrder.command;
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

namespace GoPress.Application.Features.Orders.RejectOrder.CommandHandler
{
    public class RejectOrderByShopOwnerCommandHandler : IRequestHandler<RejectOrderByShopOwnerCommand, Response<string>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<RejectOrderByShopOwnerCommandHandler> _logger;
        private readonly ICacheService _cacheService;
        public RejectOrderByShopOwnerCommandHandler(IOrderRepository orderRepository,
            ILogger<RejectOrderByShopOwnerCommandHandler> logger,
            ICacheService cacheService)
        {
            _orderRepository = orderRepository;
            _logger = logger;
            _cacheService = cacheService;
        }
        public async Task<Response<string>> Handle(RejectOrderByShopOwnerCommand request, CancellationToken cancellationToken)
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

            _logger.LogWarning(
               "ShopOwner {ShopOwnerId} rejected order {OrderId}",
             request.ShopOwnerId,
             request.OrderId);

            await _orderRepository.UpdateAsync(order);

            await _cacheService.RemoveAsync(CacheKeys.AdminDashboard);   

            _logger.LogWarning(
              "ShopOwner {ShopOwnerId} rejected order successfully  {OrderId}",
              request.ShopOwnerId,
              request.OrderId);


            return new Response<string>(
                "Order Rejected Successfully");
        }
    }
}
