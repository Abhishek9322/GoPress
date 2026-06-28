using GoPress.Application.DTOs.Admin;
using GoPress.Application.Features.Orders.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.AdminDashboard.Queries
{
    public class GetAdminDashboardQuery:IRequest<Response<AdminDashboardDto>>
    {

    }
}
