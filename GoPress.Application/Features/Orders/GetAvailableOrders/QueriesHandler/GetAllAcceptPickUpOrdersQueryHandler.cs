using GoPress.Application.DTOs.Orders;
using GoPress.Application.Features.Orders.GetAvailableOrders.Queries;
using GoPress.Application.Features.Orders.Responses;
using GoPress.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Orders.GetAvailableOrders.QueriesHandler
{
    public class GetAllAcceptPickUpOrdersQueryHandler : IRequestHandler<GetAllAcceptPickUpOrdersQuery, Response<List<AcceptPickupOrdersDto>>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<GetAllAcceptPickUpOrdersQueryHandler> _logger;
        public GetAllAcceptPickUpOrdersQueryHandler(IOrderRepository orderRepository,ILogger<GetAllAcceptPickUpOrdersQueryHandler> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }
        public async Task<Response<List<AcceptPickupOrdersDto>>> Handle(GetAllAcceptPickUpOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllAcceptedPickUpOrdersByDeliveryBoy(request.deliveryBoyId);

            var response = orders.Select(order => new AcceptPickupOrdersDto
            {
                OrderId = order.Id,
                CustomerName = order.Customer.FullName,

                CustomerPhone = order.Customer.PhoneNumber,

                PickupAddress = order.PickupAddress,

                ShopName = order.ShopOwner.ShopOwnerProfile.ShopName,

                ShopAddress = order.ShopOwner.ShopOwnerProfile.ShopAddress,

                PickupDate = order.PickupDate,

                TotalAmount = order.TotalAmount
            }).ToList();

            return new Response<List<AcceptPickupOrdersDto>>(
                response, "All Accept PickUp Order By Delivery Boy.");
        }
    }
}
