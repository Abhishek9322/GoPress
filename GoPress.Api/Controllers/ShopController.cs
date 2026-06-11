using GoPress.Api.Extensions;
using GoPress.Application.Features.Orders.GetAvailableOrders.Queries;
using GoPress.Application.Features.Orders.AcceptOrder.Comman;
using GoPress.Application.Features.Orders.RejectOrder.command;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoPress.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ShopOwner")]
    public class ShopController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ShopController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "ShopOwner")]
        [HttpPut("accept-order/{orderId}")]
        public async Task<IActionResult>AcceptOrder(int orderId)
        {
            var currentUser = User.GetCurrentUser();
            if (currentUser == null)
            {
                return Unauthorized();
            }
            var command = new AcceptOrderByShopOwnerCommand
            {
                OrderId = orderId,
               ShopOwnerId=currentUser.UserId
            };

            var response = await _mediator.Send(command);
            return Ok(response);

        }


        [Authorize(Roles = "ShopOwner")]
        [HttpPut("reject-order/{orderId}")]
        public async Task<IActionResult> RejectOrder(int orderId)
        {
            var currentUser = User.GetCurrentUser();

            var command = new RejectOrderByShopOwnerCommand
            {
                OrderId = orderId,
                ShopOwnerId = currentUser.UserId
            };

            var response =
                await _mediator.Send(command);

            return Ok(response);
        }

        [Authorize(Roles = "ShopOwner")]
        [HttpGet("ShopOwner-Orders")]
        public async Task<IActionResult> GetShopOrders()
        {
            var shopOwnerId = User.GetCurrentUser();

            var query = new GetShopOrdersQuery
            {
                ShopOwnerId = shopOwnerId.UserId
            };

            var response = await _mediator.Send(query);
            return Ok(response);
        }


    }
}
