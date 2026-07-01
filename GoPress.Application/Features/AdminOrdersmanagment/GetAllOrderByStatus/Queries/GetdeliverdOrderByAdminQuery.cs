using GoPress.Application.DTOs.Admin;
using GoPress.Application.Features.Orders.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.AdminOrdersmanagment.GetAllOrderByStatus.Queries
{
    public class GetdeliverdOrderByAdminQuery:IRequest<Response<List<AdminOrderDto>>>
    {
    }
}
