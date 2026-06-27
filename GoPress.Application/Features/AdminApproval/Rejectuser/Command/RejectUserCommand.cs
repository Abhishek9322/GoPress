using GoPress.Application.Features.Orders.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.AdminApproval.Rejectuser.Command
{
    public class RejectUserCommand:IRequest<Response<string>>
    {
        public int UserId { get; set; }
        public string? Reason { get; set; }
    }
}
