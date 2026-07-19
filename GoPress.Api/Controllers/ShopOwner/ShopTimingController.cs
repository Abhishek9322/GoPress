using GoPress.Api.Extensions;
using GoPress.Application.DTOs.Shops;
using GoPress.Application.Features.ShopOpration.UpdateShopTime.Command;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoPress.Api.Controllers.ShopOwner
{
    [Route("api/ShopTiming")]
    [ApiController]
    [Authorize(Roles = "ShopOwner")]
    public class ShopTimingController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ShopTimingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateShopTiming(UpdateShopTimingDto UpdateTime)
        {
            var currentUser = User.GetCurrentUser();

            var response = await _mediator.Send(
                new UpdateShopTimeCommand
                {
                    ShopOwnerId = currentUser.UserId,
                    Timing = UpdateTime
                });

            return Ok(response);
        }
    }
}
