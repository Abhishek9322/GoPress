using GoPress.Application.DTOs.Orders;
using GoPress.Application.Features.Orders.GetAvailableOrders.Queries;
using GoPress.Application.Features.Orders.Responses;
using GoPress.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Orders.GetAvailableOrders.QueriesHandler
{
    public class GetAllOutForDeliveryOrdersByDBoyQueryHandler : IRequestHandler<GetAllOutForDeliveryOrdersByDBoyQuery, Response<List<ReadyForDeliveryOrderDto>>>
    {
        private readonly IOrderRepository _orderRepository;
        public GetAllOutForDeliveryOrdersByDBoyQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Response<List<ReadyForDeliveryOrderDto>>> Handle(GetAllOutForDeliveryOrdersByDBoyQuery request, CancellationToken cancellationToken)
        {
          var orders=await _orderRepository.GetAllOutForDeliveryOrdersByDeliveryBoy(request.deliveryBoyId);
            var result = orders.Select(order => new ReadyForDeliveryOrderDto
            {
                OrderId = order.Id,
                CustomerName = order.Customer.FullName,
                CustomerPhone = order.Customer.PhoneNumber,
                PickupAddress = order.PickupAddress,
                DeliveryAddress = order.DeliveryAddress,
                TotalAmount = order.TotalAmount,
                Status = order.Status.ToString()
            }).ToList();

            return new Response<List<ReadyForDeliveryOrderDto>>(result, "All Out For Delivery Orders");
        }
    }
}
