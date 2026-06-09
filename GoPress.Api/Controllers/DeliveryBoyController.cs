using GoPress.Api.Extensions;
using GoPress.Application.Features.Orders.Commands;
using GoPress.Application.Features.Orders.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoPress.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "DeliveryBoy")]
    public class DeliveryBoyController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DeliveryBoyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        
        [HttpGet("available-orders")]   
        public async Task<IActionResult> GetAvailableOrders()
        {
            var query = new GetAvailableOrdersQuery();  

            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPut("accept-pickup/{orderId}")]
        public async Task<IActionResult> AcceptPickup(int orderId)
        {
            var currentUser = User.GetCurrentUser();

            var command = new AcceptPickupCommand
                {
                    OrderId = orderId,
                    DeliveryBoyId = currentUser.UserId
                };

            var response =await _mediator.Send(command);

            return Ok(response);
        }
    }
}
