using GoPress.Application.DTOs.Orders;
using GoPress.Application.Features.Orders.GetAvailableOrders.Queries;
using GoPress.Application.Features.Orders.Responses;
using GoPress.Application.Interfaces.Repositories;
using GoPress.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Orders.GetAvailableOrders.QueriesHandler
{
    public class GetReadyForDeliveryOrdersQueryHandler : IRequestHandler<GetReadyForDeliveryOrdersQuery, Response<List<ReadyForDeliveryOrderDto>>>
    {
        private readonly IOrderRepository _orderRepository;
        public GetReadyForDeliveryOrdersQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository=orderRepository;
        }
        public async Task<Response<List<ReadyForDeliveryOrderDto>>> Handle(GetReadyForDeliveryOrdersQuery request, CancellationToken cancellationToken)
        {
            var order=await _orderRepository.GetReadyForDeliveryOrdersAsync(request.DeliveryBoyId);
            var result =
                 order.Select(orders =>
                     new ReadyForDeliveryOrderDto
                     {
                         OrderId = orders.Id,
                         CustomerName =
                             orders.Customer.FullName,
                         CustomerPhone =
                             orders.Customer.PhoneNumber,
                         DeliveryAddress =
                             orders.DeliveryAddress,
                         TotalAmount =
                             orders.TotalAmount
                     })
                 .ToList();

            return new Response<List<ReadyForDeliveryOrderDto>>(result, "Ready For Delivery Orders");
        }
    }
}
