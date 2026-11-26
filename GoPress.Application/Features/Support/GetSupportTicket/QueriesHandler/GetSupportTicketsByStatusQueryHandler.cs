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
    public class GetSupportTicketsByStatusQueryHandler : IRequestHandler<GetSupportTicketsByStatusQuery, Response<List<SupportTicketDto>>>
    {
        private readonly ISupportTicketRepository _supportTicketRepository;
        private readonly ILogger<GetSupportTicketsByStatusQueryHandler> _logger;
        public GetSupportTicketsByStatusQueryHandler(ILogger<GetSupportTicketsByStatusQueryHandler> loger,ISupportTicketRepository supportTicketRepository)
        {
            _logger = loger;
            _supportTicketRepository = supportTicketRepository;
        }
        public async Task<Response<List<SupportTicketDto>>> Handle(GetSupportTicketsByStatusQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Fetching support tickets with status: {Status}",
                request.Status);

            var ticket = await _supportTicketRepository.GetByStatusAsyns(request.Status);

            var ticketDtos = ticket.Select(ticket => new SupportTicketDto
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
                "Retrieved {TicketCount} support tickets with status {Status}",
                ticketDtos.Count,
                request.Status);

            return new Response<List<SupportTicketDto>>(ticketDtos,"Support tickets retrieved successfully");
        }
    }
}
