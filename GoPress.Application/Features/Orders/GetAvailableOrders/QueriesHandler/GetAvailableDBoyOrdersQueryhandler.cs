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
    public class GetAvailableDBoyOrdersQueryhandler : IRequestHandler<GetAvailableDBoyOrdersQuery, Response<List<AvailableOrderDto>>>
    {
        private readonly IOrderRepository _orderRepository;
        public GetAvailableDBoyOrdersQueryhandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Response<List<AvailableOrderDto>>> Handle(GetAvailableDBoyOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAvailableOrdersAsync();  //Available Order to accept for d boy here

            var result=orders.Select(orders=>
               new AvailableOrderDto
               {
                   OrderId = orders.Id,

                   CustomerName = orders.Customer.FullName,

                   CustomerPhone = orders.Customer.PhoneNumber,

                   PickupAddress = orders.PickupAddress,

                   ShopName =  orders.ShopOwner.ShopOwnerProfile.ShopName,

                   ShopAddress =orders.ShopOwner.ShopOwnerProfile.ShopAddress,

                   PickupDate = orders.PickupDate,

                   TotalAmount = orders.TotalAmount
               }).ToList();

            return new Response<List<AvailableOrderDto>>(result, "Available Orders");
        }
    }
}
