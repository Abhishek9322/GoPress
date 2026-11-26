using GoPress.Application.DTOs.Support;
using GoPress.Application.Features.Orders.Responses;
using GoPress.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Support.GetSupportTicket.Queries
{
    public class GetSupportTicketsByStatusQuery:IRequest<Response<List<SupportTicketDto>>>
    {
        public SupportTicketStatusEnum Status { get; set; }
    }

}
