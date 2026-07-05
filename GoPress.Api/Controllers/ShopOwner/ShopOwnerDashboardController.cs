using GoPress.Api.Extensions;
using GoPress.Application.Features.DashBoard.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoPress.Api.Controllers.ShopOwner
{
    [Route("api/ShopOwner/Dashboard")]
    [ApiController]
    [Authorize(Roles = "ShopOwner")]
    public class ShopOwnerDashboardController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ShopOwnerDashboardController(IMediator mediator)
        {
            _mediator=mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            var currentUser = User.GetCurrentUser();

            var response = await _mediator.Send(new GetShopOwnerDashBoardQuery
            {
                ShopOwnerId = currentUser.UserId
            });

            return Ok(response);


        }

    }
}
