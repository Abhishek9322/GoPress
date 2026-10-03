using GoPress.Api.Extensions;
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


        [HttpGet("{customerId}/Customer")]
        public async Task<IActionResult> GetCustomerDetails(int customerId)
        {
            var query = new GetCustomerDetailsQuery
            {
                CustomerId = customerId,
            };
            var response=await _mediator.Send(query);
            return Ok(response);
        }
    }
}
