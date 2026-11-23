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
    public class GetAllAdminSupportTicketQueryHandler : IRequestHandler<GetAllAdminSupportTicketQuery, Response<List<SupportTicketDto>>>
    {
        private readonly ISupportTicketRepository _supportTicketRepository;
        private readonly ILogger<GetAllAdminSupportTicketQueryHandler> _logger;
        public GetAllAdminSupportTicketQueryHandler(ISupportTicketRepository supportTicketRepository,Logger<GetAllAdminSupportTicketQueryHandler> logger)
        {
            _supportTicketRepository = supportTicketRepository;
            _logger = logger;
        }
        public async Task<Response<List<SupportTicketDto>>> Handle(GetAllAdminSupportTicketQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Admin requested all support tickets.");

            var AllTicket = await _supportTicketRepository.GetAllSupportTicketAsync();

            var ticketDtos = AllTicket.Select(ticket => new SupportTicketDto
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

            }).ToList();

            _logger.LogInformation(
               "Retrieved {TicketCount} support tickets for admin.",
               ticketDtos.Count);

            return new Response<List<SupportTicketDto>>(ticketDtos, "All support tickets retrieved successfully");
        }
    }
}
