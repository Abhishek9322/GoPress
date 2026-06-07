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
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery,Response<List<OrderResponseDto>>>
    {
        private readonly IOrderRepository _orderRepository;
        public GetAllOrdersQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Response<List<OrderResponseDto>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllOrdersAsync();

            var response=orders.Select(order => new OrderResponseDto
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
               .Select(x => new OrderItemResponseDto
               {
                   Id = x.Id,
                   ClothName = x.ClothName,
                   Quantity = x.Quantity,
                   Price = x.Price,
                   TotalPrice = x.TotalPrice
               }).ToList()
            }).ToList();

            return new Response<List<OrderResponseDto>>(
                response,
                "All Orders Retrieved Successfully");
        }
    }
}
