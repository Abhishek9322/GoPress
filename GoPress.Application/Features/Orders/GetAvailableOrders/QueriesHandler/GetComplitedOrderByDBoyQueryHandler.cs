using GoPress.Application.DTOs.Orders;
using GoPress.Application.Features.Orders.GetAvailableOrders.Queries;
using GoPress.Application.Features.Orders.Responses;
using GoPress.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Orders.GetAvailableOrders.QueriesHandler
{
    public class GetComplitedOrderByDBoyQueryHandler : IRequestHandler<GetComplitedOrderByDBoyQuery, Response<List<DeliveredOrderDto>>>
    {
        private readonly IOrderRepository _orderRepository;
        public GetComplitedOrderByDBoyQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Response<List<DeliveredOrderDto>>> Handle(GetComplitedOrderByDBoyQuery request, CancellationToken cancellationToken)
        {
            var orders =await _orderRepository.GetDeliveredOrdersByDeliveryBoyAsync(request.DeliveryBoyId);

            var result = orders.Select(order => new DeliveredOrderDto
                {
                    OrderId = order.Id,
                    CustomerName = order.Customer.FullName,
                    CustomerPhone = order.Customer.PhoneNumber,
                    ShopName =
                        order.ShopOwner.ShopOwnerProfile.ShopName,
                    DeliveryAddress = order.DeliveryAddress,
                    TotalAmount = order.TotalAmount,
                    DeliveryDate = order.DeliveryDate
                }).ToList();  

            return new Response<List<DeliveredOrderDto>>(result, "Delivered Order ");
        }
    }
}
