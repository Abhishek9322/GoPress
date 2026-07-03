using GoPress.Api.Extensions;
using GoPress.Application.Features.Profile.CustomerProfile.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoPress.Api.Controllers.Customer
{
    [Route("api/Customer/Profiles")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class CustomerProfileController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomerProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            var currentuser = User.GetCurrentUser();

            var response = await _mediator.Send(new GetCustomerProfileQuery
            {
                userId = currentuser.UserId
            });

            return Ok(response);
        }
    }
}
