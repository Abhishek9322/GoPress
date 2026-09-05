using GoPress.Application.DTOs.Orders;
using GoPress.Application.Features.Orders.Responses;
using MediatR;

namespace GoPress.Application.Features.Orders.GetAvailableOrders.Queries
{
    public class GetAllAcceptPickUpOrdersQuery:IRequest<Response<List<AcceptPickupOrdersDto>>>
    {
        public int deliveryBoyId { get; set; }
    }
}
