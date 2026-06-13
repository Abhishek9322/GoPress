using GoPress.Application.DTOs.Orders;
using GoPress.Application.Features.Orders.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Orders.GetAvailableOrders.Queries
{
    public class GetReadyForDeliveryOrdersQuery:IRequest<Response<List<ReadyForDeliveryOrderDto>>>
    {
        public int DeliveryBoyId { get; set; }
    }
}
