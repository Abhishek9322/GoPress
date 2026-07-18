using GoPress.Api.Extensions;
using GoPress.Application.Features.Orders.GetAvailableOrders.GetShop.Queries;
using GoPress.Application.Features.Orders.GetAvailableOrders.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoPress.Api.Controllers.Customer
{
    [Route("api/CustomerShop")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class CustomerShopController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomerShopController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("shop-owner/{shopOwnerId}/price-list")]
        public async Task<IActionResult> GetShopOwnerPriceList(int shopOwnerId)
        {
            var currentUser = User.GetCurrentUser();

            var query = new GetShopOwnerPriceListQuery
            {
                ShopOwnerId = shopOwnerId

            };
            var response = await _mediator.Send(query);

            return Ok(response);

        }

        [HttpGet]
        public async Task<IActionResult> GetAvailableShops()
        {
            var currentUser = User.GetCurrentUser();

            var response = await _mediator.Send(
                new GetAvailableShopsQuery
                {
                    CustomerId = currentUser.UserId
                });

            return Ok(response);
        }


    }
}
