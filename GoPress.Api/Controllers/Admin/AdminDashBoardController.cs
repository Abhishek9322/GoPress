using GoPress.Application.Features.DashBoard.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoPress.Api.Controllers.Admin
{
    [Route("api/Admin/Dashboard")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminDashBoardController : ControllerBase
    {

        private readonly IMediator _mediator;
        public AdminDashBoardController(IMediator mediator)
        {
            _mediator = mediator;
        }
       
        [HttpGet("Get-dashboard")]
        public async Task<IActionResult> GetDashboard()
        {
            var response =
                await _mediator.Send(
                    new GetAdminDashboardQuery());

            return Ok(response);
        }
    }
}
