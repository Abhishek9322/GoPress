using GoPress.Application.DTOs.Orders;
using GoPress.Application.Features.Orders.Responses;
using MediatR;

namespace GoPress.Application.Features.Orders.GetAvailableOrders.Queries
{
    public class GetAllPickupCompletedOrdersByDeliveryBoyQuery:IRequest<Response<List<OrderResponseDto>>>
    {
        public int deliveryBoyId { get; set; }
    }
}
