using GoPress.Application.DTOs.Orders;
using GoPress.Application.Features.Orders.Queries;
using GoPress.Application.Features.Orders.Responses;
using GoPress.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Orders.QueriesHandler
{
    public class GetDeliveryOrdersQueryHandler : IRequestHandler<GetDeliveryOrdersQuery, Response<List<OrderResponseDto>>>
    {
        private readonly IOrderRepository _orderRepository;
        public GetDeliveryOrdersQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Response<List<OrderResponseDto>>> Handle(GetDeliveryOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetDeliveryOrdersAsync(request.DeliveryBoyId);

            var response=orders.Select(order=>new OrderResponseDto
            {
                Id = order.Id,

                CustomerId = order.CustomerId,

                ShopOwnerId = order.ShopOwnerId,

                DeliveryBoyId = order.DeliveryBoyId,

                PickupAddress = order.PickupAddress,

                DeliveryAddress = order.DeliveryAddress,

                PickupDate = order.PickupDate,

                DeliveryDate = order.DeliveryDate,

                TotalAmount = order.TotalAmount,

                Notes = order.Notes,

                Status = order.Status,

                OrderItems = order.OrderItems
                    .Select(item => new OrderItemResponseDto
                    {
                        Id = item.Id,

                        ClothName = item.ClothName,

                        Quantity = item.Quantity,

                        Price = item.Price,

                        TotalPrice = item.TotalPrice

                    }).ToList()

            }).ToList();

            return new Response<List<OrderResponseDto>>
                 (
                     response,
                     "Delivery Orders Retrieved Successfully"
                 );
        }
    }
}
