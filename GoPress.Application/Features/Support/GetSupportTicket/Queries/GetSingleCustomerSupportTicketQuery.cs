using GoPress.Application.DTOs.Support;
using GoPress.Application.Features.Orders.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Support.GetSupportTicket.Queries
{
    public class GetSingleCustomerSupportTicketQuery:IRequest<Response<SupportTicketDto>>
    {
        public int CustomerId { get; set; } 
        public int TicketId { get; set; }
    }
}
