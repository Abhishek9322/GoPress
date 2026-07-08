using GoPress.Application.Comman.Caching;
using GoPress.Application.Features.Orders.AcceptOrder.Command;
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

namespace GoPress.Application.Features.Orders.AcceptOrder.CommandHandler
{
    public class PickupCompletedCommandHandler : IRequestHandler<PickupCompletedCommand, Response<string>>
    {
        private readonly IOrderRepository _orderRespository;
        private readonly ICacheService _cacheService;
        public PickupCompletedCommandHandler(IOrderRepository orderRepository,
            ICacheService cacheService)
        {
            _orderRespository = orderRepository;
            _cacheService = cacheService;
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

            await _cacheService.RemoveAsync(CacheKeys.AdminDashboard);
            await _cacheService.RemoveAsync(CacheKeys.ShopOwnerDashboard);
            await _cacheService.RemoveAsync(CacheKeys.DeliveryBoyDashboard);
            await _cacheService.RemoveAsync(CacheKeys.CustomerDashboard);

            return new Response<string>("Clothes Picked Up Successfully");



        }
    }
}
