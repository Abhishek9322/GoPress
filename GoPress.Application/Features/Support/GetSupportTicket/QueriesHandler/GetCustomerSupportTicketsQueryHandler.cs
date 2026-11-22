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
    public class GetCustomerSupportTicketsQueryHandler : IRequestHandler<GetCustomerSupportTicketsQuery, Response<List<SupportTicketDto>>>
    {
        private readonly ILogger<GetCustomerSupportTicketsQueryHandler> _logger;
        private readonly ISupportTicketRepository _supportTicketRepository;
        public GetCustomerSupportTicketsQueryHandler(ILogger<GetCustomerSupportTicketsQueryHandler> logger, ISupportTicketRepository supportTicketRepository)
        {
            _logger = logger;
            _supportTicketRepository = supportTicketRepository;
        }
        public async Task<Response<List<SupportTicketDto>>> Handle(GetCustomerSupportTicketsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Getting support tickets for Customer {CustomerId}",
                request.CustomerId);

            var tikets=await _supportTicketRepository.GetByCustomerIdAsync(request.CustomerId);

            var ticketDtos = tikets.Select(t => new SupportTicketDto
            {
                Id = t.Id,
                TicketNumber = t.TicketNumber,
                OrderId = t.OrderId,
                Subject = t.Subject,
                Description = t.Description,
                Category = t.Category,
                Status = t.Status,
                Priority = t.Priority,
                AssignedAdminId = t.AssignedAdminId,
                CreatedAt = t.CreatedAt,
                ResolvedAt = t.ResolvedAt,
                ClosedAt = t.ClosedAt
            }).ToList();

            _logger.LogInformation(
              "Retrieved {TicketCount} support tickets for Customer {CustomerId}",
              ticketDtos.Count,
              request.CustomerId);

            return new Response<List<SupportTicketDto>>(ticketDtos, "Customer support tickets retrieved successfully");
           
        }
    }
}
