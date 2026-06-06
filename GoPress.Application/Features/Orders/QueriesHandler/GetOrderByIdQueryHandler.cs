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
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderById, Response<OrderResponseDto>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Response<OrderResponseDto>> Handle(GetOrderById request, CancellationToken cancellationToken)
        {
            var order =await _orderRepository.GetByIdAsync(request.OrderId);

            if (order == null)
            {
                return new Response<OrderResponseDto>("Order Not Found");
            }
            var response = new OrderResponseDto
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
            };

            return new Response<OrderResponseDto>(
               response,
               "Order Retrieved Successfully");
        }
    }
}
