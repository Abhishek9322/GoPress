using GoPress.Api.Extensions;
using GoPress.Application.DTOs.Support;
using GoPress.Application.Features.Support.CreateSupportTicket.Command;
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

            return Ok(response);
        }

    }


}
