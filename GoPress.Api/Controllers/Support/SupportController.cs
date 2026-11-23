using GoPress.Api.Extensions;
using GoPress.Application.DTOs.Support;
using GoPress.Application.Features.Support.CreateSupportTicket.Command;
using GoPress.Application.Features.Support.GetSupportTicket.Queries;
using GoPress.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace GoPress.Api.Controllers.Support
{
    [Route("api/Support")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class SupportController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SupportController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Create-Ticket")]
        public async Task<IActionResult> CreateSupportTicket(CreateSupportTicketDto request)
        {
            var currentUser= User.GetCurrentUser();

            var response = await _mediator.Send(new CreateSupportTicketCommand
            {
                CustomerId = currentUser.UserId,
                Ticket = request
            });

            if(!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("Get-All-Tickets")]
        public async Task<IActionResult> GetAllCustomerTickets()
        {
            var currentUser = User.GetCurrentUser();    

            var response=await _mediator.Send(new GetCustomerSupportTicketsQuery
            {
                CustomerId = currentUser.UserId
            });

            return Ok(response);
        }

        [HttpGet("Get-byId")]
        public async Task<IActionResult> GetCustomerTicketById(int ticketId)
        {
            var currentUser=User.GetCurrentUser();

            var response = await _mediator.Send(new GetSingleSupportTicketQuery
            {
                CustomerId = currentUser.UserId,
                TicketId= ticketId
            });

            if(response.Data==null)
            {
                return BadRequest(response);    
            }

            return Ok(response);
        }

    }


}
