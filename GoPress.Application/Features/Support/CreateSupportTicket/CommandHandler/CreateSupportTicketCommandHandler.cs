using GoPress.Application.Features.Orders.Responses;
using GoPress.Application.Features.Support.CreateSupportTicket.Command;
using GoPress.Application.Interfaces.Repositories;
using GoPress.Domain.Entities;
using GoPress.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Support.CreateSupportTicket.CommandHandler
{
    public class CreateSupportTicketCommandHandler : IRequestHandler<CreateSupportTicketCommand, Response<int>>
    {
        private readonly ILogger<CreateSupportTicketCommandHandler> _logger;
        private readonly ISupportTicketRepository _supportTicket;
        private readonly IOrderRepository _orderRepository;
        public CreateSupportTicketCommandHandler(ILogger<CreateSupportTicketCommandHandler> logger, ISupportTicketRepository supportTicket)
        {
            _logger = logger;
            _supportTicket = supportTicket;
            _orderRepository = _orderRepository;
        }
        public async Task<Response<int>> Handle(CreateSupportTicketCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
               "Customer {CustomerId} is creating a support ticket for Order {OrderId}",
               request.CustomerId,
               request.Ticket.OrderId);

            var order=await _supportTicket.GetCustomerOrderForSupportAsync(request.CustomerId, request.Ticket.OrderId);

            if (order == null)
            {
                {
                    _logger.LogWarning(
                     "Customer {CustomerId} attempted to create a support ticket for invalid Order {OrderId}",
                     request.CustomerId,
                     request.Ticket.OrderId);

                    return new Response<int>("Invalid order or unauthorized access");
                }
            }

            var ticketNumber = $"GP-SUP-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString("N")[..8].ToUpper()}";

            var supportTicket = new SupportTicket
            {
                TicketNumber = ticketNumber,
                OrderId = order.Id,
                Subject = request.Ticket.Subject,
                Description = request.Ticket.Description,
                Category = request.Ticket.Category,
                Status = SupportTicketStatusEnum.Open,
                Priority = SupportPriorityEnum.Medium,
                CreatedAt = DateTime.UtcNow
            };

            var createdTicket = await _supportTicket.CreateSupportTicketAsync(supportTicket);


            _logger.LogInformation(
                "Support ticket {TicketNumber} created successfully for Customer {CustomerId} and Order {OrderId}",
                createdTicket.TicketNumber,
                request.CustomerId,
                order.Id);

            return new Response<int>(createdTicket.Id,"Support ticket created successfully ");
        }
    }
}
