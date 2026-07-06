using GoPress.Application.DTOs.Dashboard;
using GoPress.Application.Features.Orders.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.DashBoard.Queries
{
    public class GetDeliveryBoyDashboardQuery:IRequest<Response<DeliveryBoyDashBoardDto>>
    {
        public int DeliveryBoyId { get; set; }
    }
}
