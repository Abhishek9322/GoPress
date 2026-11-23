using GoPress.Application.Features.Support.GetSupportTicket.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoPress.Api.Controllers.Support
{
    [Route("api/admin/support")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class AdminSupportController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AdminSupportController(IMediator mediator)
        {
            _mediator = mediator;   
        }

        [HttpGet("Get-All-Tiket")]
        public async Task<IActionResult> GetAllAdminTicket()
        {
            var response = await _mediator.Send(new GetAllAdminSupportTicketQuery());
            if (response == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
