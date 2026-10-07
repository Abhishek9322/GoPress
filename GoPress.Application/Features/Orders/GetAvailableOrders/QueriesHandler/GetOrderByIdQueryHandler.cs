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
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Response<OrderResponseDto>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Response<OrderResponseDto>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order =await _orderRepository.GetOrderDetailsbyAdmin(request.OrderId);

            if (order == null)
            {
                return new Response<OrderResponseDto>("Order Not Found");
            }
            var response = new OrderResponseDto
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                CustomerName=order.Customer?.FullName,
                ShopOwnerId = order.ShopOwnerId,
                ShopOwnerName=order.ShopOwner?.FullName,
                DeliveryBoyId = order.DeliveryBoyId,
                DeliveryBoyName=order.DeliveryBoy?.FullName,
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
                   ClothTypeId=x.ClothTypeId,
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
