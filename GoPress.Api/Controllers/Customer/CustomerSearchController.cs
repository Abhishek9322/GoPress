using GoPress.Api.Extensions;
using GoPress.Application.Features.ShopOpration.GetShop.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoPress.Api.Controllers.Customer
{
    [Route("api/CustomerSearch")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class CustomerSearchController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomerSearchController(IMediator mediator)
        {
            _mediator= mediator;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string keyword)
        {
            var currentuser = User.GetCurrentUser();
            var response = await _mediator.Send(
                new GetShopSearchQuery
                {
                    Keyword = keyword,
                    CustomerId = currentuser.UserId
                });

            return Ok(response);
        }
    }
}
