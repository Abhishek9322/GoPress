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
    public class GetAllPendingOrdersByShopOwnerQueryHandler : IRequestHandler<GetAllPendingOrdersByShopOwnerQuery, Response<List<ShopOrderDto>>>
    {
        private readonly IOrderRepository _orderRepository;
        public GetAllPendingOrdersByShopOwnerQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Response<List<ShopOrderDto>>> Handle(GetAllPendingOrdersByShopOwnerQuery request, CancellationToken cancellationToken)
        {
          var orders =await _orderRepository.GetAllPendingOrdersByShopOwner(request.ShopownerId);

            var response=orders.Select(order=>new ShopOrderDto
            {
                OrderId=order.Id,
                CustomerName=order.Customer.FullName,
                CustomerPhone=order.Customer.PhoneNumber,
                PickupAddress=order.PickupAddress,
                DeliveryAddress=order.DeliveryAddress,
                TotalAmount=order.TotalAmount,
                Status=order.Status.ToString()
            }).ToList();


            return new Response<List<ShopOrderDto>>(response, "Pending Orders Fetch Successfully .");
            
        }
    }
}
