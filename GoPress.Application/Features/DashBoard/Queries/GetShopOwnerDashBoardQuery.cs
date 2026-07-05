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
    public class GetShopOwnerDashBoardQuery:IRequest<Response<ShopOwnerDashBoardDto>>
    {
        public int ShopOwnerId { get; set; }
    }
}
