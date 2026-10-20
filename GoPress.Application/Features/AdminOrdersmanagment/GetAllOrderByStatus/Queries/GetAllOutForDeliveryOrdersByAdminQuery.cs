using GoPress.Application.DTOs.Admin;
using GoPress.Application.Features.Orders.Responses;
using MediatR;

namespace GoPress.Application.Features.AdminOrdersmanagment.GetAllOrderByStatus.Queries
{
    public class GetAllOutForDeliveryOrdersByAdminQuery:IRequest<Response<List<AdminOrderDto>>>
    {

    }
}
