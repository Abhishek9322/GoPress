using GoPress.Api.Extensions;
using GoPress.Application.Features.DashBoard.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace GoPress.Api.Controllers.Customer
{
    [Route("api/Customer/Dashboard")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class CustomerDashboardController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomerDashboardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> CustomerDashboard()
        {
            var currentuser = User.GetCurrentUser();

            var response = await _mediator.Send(new GetCustomerDashboardQuery
            {
                customerUserId = currentuser.UserId
            });


            return Ok(response);
        }
    }
}
