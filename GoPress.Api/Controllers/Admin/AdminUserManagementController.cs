using GoPress.Application.Features.AdminApproval.AdminUserManagement.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoPress.Api.Controllers.Admin
{
    [Route("api/Admin/UserManagment")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminUserManagementController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AdminUserManagementController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("ALL-Customer")]
        public async Task<IActionResult> GetAllCustomerProfile()
        {
            var response = await _mediator.Send(new GetAllCustomerProfileQuery());
            return Ok(response);
        }
    }
}
