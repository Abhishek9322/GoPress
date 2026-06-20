using GoPress.Api.Extensions;
using GoPress.Application.DTOs.Orders;
using GoPress.Application.Features.Orders.CreateOrder.Command;
using GoPress.Application.Features.Orders.GetAvailableOrders.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoPress.Api.Controllers.ShopOwner
{
    [Route("api/ShopOwner/ClothPrice")]
    [ApiController]
    [Authorize(Roles = "ShopOwner")]
    public class ShopOwnerClothPriceController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ShopOwnerClothPriceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Cloth-price")]
        public async Task<IActionResult> AddClothPrice(AddClothPriceDto addClothPrice)
        {
            var currentuser = User.GetCurrentUser();

            var command = new AddClothPriceCommand
            {
                ShopOwnerId = currentuser.UserId,
                Price = addClothPrice
            };

            var response = await _mediator.Send(command);

            return Ok(response);

        }

        [HttpGet("my-price-list")]
        public async Task<IActionResult> GetMyPriceList()
        {
            var currentUser = User.GetCurrentUser();

            var query = new GetMyPriceListQuery
            {
                ShopOwnerId = currentUser.UserId
            };

            var respose = await _mediator.Send(query);

            return Ok(respose);

        }


        [HttpPut("{priceId}/update-price")]
        public async Task<IActionResult>UpdatePrice(int priceId,UpdateClothPriceDto clothPriceDto)
        {
            var currentuser = User.GetCurrentUser();

            var command = new UpdateClothPriceCommand
            {
                ShopOwnerId = currentuser.UserId,
                PriceId = priceId,
                UpdatePrice = clothPriceDto
            };

            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpDelete("cloth-price/{priceId}")]
        public async Task<IActionResult> DeletePrice(int priceId)
        {
            var currentUser =User.GetCurrentUser();

              var command =new DeleteClothPriceCommand
                {
                    PriceId = priceId,
                    ShopOwnerId = currentUser.UserId
                };

            var response =await _mediator.Send(command);

            return Ok(response);
        }
    }
}
