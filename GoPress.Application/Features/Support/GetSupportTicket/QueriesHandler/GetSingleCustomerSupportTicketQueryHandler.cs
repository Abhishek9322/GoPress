using GoPress.Application.DTOs.Support;
using GoPress.Application.Features.Orders.Responses;
using GoPress.Application.Features.Support.GetSupportTicket.Queries;
using GoPress.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Support.GetSupportTicket.QueriesHandler
{
    public class GetSingleCustomerSupportTicketQueryHandler : IRequestHandler<GetSingleCustomerSupportTicketQuery, Response<SupportTicketDto>>
    {
        private readonly ISupportTicketRepository _supportTicketRepository;
        private readonly ILogger<GetCustomerSupportTicketsQueryHandler> _logger;
        public GetSingleCustomerSupportTicketQueryHandler(ISupportTicketRepository supportTicketRepository,ILogger<GetCustomerSupportTicketsQueryHandler> logger)
        {
            _logger= logger;
            _supportTicketRepository= supportTicketRepository;
        }
        public async Task<Response<SupportTicketDto>> Handle(GetSingleCustomerSupportTicketQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Customer {CustomerId} is requesting Support Ticket {TicketId}",
                request.CustomerId,
                request.TicketId);

            var ticket=await _supportTicketRepository.GetCustomerTicketByIdAsync(request.CustomerId,request.TicketId);

            if (ticket == null)
            {

                _logger.LogWarning(
                    "Support Ticket {TicketId} not found for Customer {CustomerId}",
                    request.TicketId,
                    request.CustomerId);

                return new Response<SupportTicketDto>(
                    "Support ticket not found or unauthorized access");
            }

            var ticketDto = new SupportTicketDto
            {
                Id = ticket.Id,
                TicketNumber = ticket.TicketNumber,
                OrderId = ticket.OrderId,
                Subject = ticket.Subject,
                Description = ticket.Description,
                Category = ticket.Category,
                Status = ticket.Status,
                Priority = ticket.Priority,
                AssignedAdminId = ticket.AssignedAdminId,
                CreatedAt = ticket.CreatedAt,
                ResolvedAt = ticket.ResolvedAt,
                ClosedAt = ticket.ClosedAt
            };

            _logger.LogInformation(
                    "Support Ticket {TicketId} Successfully Found ",
                    request.TicketId);

            return new Response<SupportTicketDto>(ticketDto, "Support ticket retrieved successfully");
        }
    }
}
