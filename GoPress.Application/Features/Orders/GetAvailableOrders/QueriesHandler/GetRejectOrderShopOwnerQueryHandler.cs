using GoPress.Application.DTOs.Orders;
using GoPress.Application.Features.Orders.GetAvailableOrders.Queries;
using GoPress.Application.Features.Orders.Responses;
using GoPress.Application.Interfaces.Repositories;
using MediatR;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Orders.GetAvailableOrders.QueriesHandler
{
    public class GetRejectOrderShopOwnerQueryHandler : IRequestHandler<GetRejectOrderShopOwnerQuery, Response<List<ShopOrderDto>>>
    {
        private readonly IOrderRepository _orderRepository;
        public GetRejectOrderShopOwnerQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Response<List<ShopOrderDto>>> Handle(GetRejectOrderShopOwnerQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetRejectedOrdersByShopOwnerAsync(request.ShopOwnerId);

            var result = order.Select(orders => new ShopOrderDto
            {

                OrderId = orders.Id,
                CustomerName = orders.Customer.FullName,
                CustomerPhone = orders.Customer.PhoneNumber,
                PickupAddress = orders.PickupAddress,
                DeliveryAddress = orders.DeliveryAddress,
                TotalAmount = orders.TotalAmount,
                Status = orders.Status.ToString()

            }).ToList();

            return new Response<List<ShopOrderDto>>(result, "All Rejected Order");
        }
    }
}
