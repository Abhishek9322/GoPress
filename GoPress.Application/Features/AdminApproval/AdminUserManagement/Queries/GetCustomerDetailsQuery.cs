using GoPress.Application.DTOs.Admin.UserManegment;
using GoPress.Application.Features.Orders.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.AdminApproval.AdminUserManagement.Queries
{
    public class GetCustomerDetailsQuery:IRequest<Response<AllCutomerProfileDto>>
    {
        public int CustomerId { get; set; }
    }
}
