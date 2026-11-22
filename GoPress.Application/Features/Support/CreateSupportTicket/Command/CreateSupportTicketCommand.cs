using GoPress.Application.DTOs.Support;
using GoPress.Application.Features.Orders.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Support.CreateSupportTicket.Command
{
    public class CreateSupportTicketCommand: IRequest<Response<int>>
    {
        public CreateSupportTicketDto Ticket{ get; set; }
        public int CustomerId { get; set; }    
    }
}
