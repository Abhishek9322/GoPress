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
    public class AcceptOrderByShopOwnerCommandHandler : IRequestHandler<AcceptOrderByShopOwnerCommand, Response<string>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<AcceptOrderByShopOwnerCommandHandler> _logger;
        private readonly ICacheService _cacheService;
        public AcceptOrderByShopOwnerCommandHandler(IOrderRepository orderRepository,
            ILogger<AcceptOrderByShopOwnerCommandHandler> logger,
            ICacheService cacheService)
        {
            _orderRepository = orderRepository;
            _logger = logger;
            _cacheService = cacheService;
        }
        public async Task<Response<string>> Handle(AcceptOrderByShopOwnerCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);

            if(order==null)
            {
                return new Response<string>("Order Not Found");
            }

            if(order.ShopOwnerId!=request.ShopOwnerId)
            {
                return new Response<string>("You cannot access this order");
            }

            if (order.Status != OrderStatusEnum.Pending)
            {
                return new Response<string>("Only pending orders can be accepted");
            }

            order.Status = OrderStatusEnum.Accepted;

            _logger.LogInformation(
               "ShopOwner {ShopOwnerId} accepted order {OrderId}",
               request.ShopOwnerId,
               request.OrderId);

            await _orderRepository.UpdateAsync(order);

            //remove cache for admin dashboard to reflect the updated order status
            await _cacheService.RemoveAsync(CacheKeys.AdminDashboard);
            await _cacheService.RemoveAsync(CacheKeys.ShopOwnerDashboard);

            _logger.LogInformation(
              "ShopOwner {ShopOwnerId} accepted order successfully  {OrderId}",
              request.ShopOwnerId,
              request.OrderId);


            return new Response<string>("Order Accepted Successfully");
        }
    }
}
