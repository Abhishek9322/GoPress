using GoPress.Api.Extensions;
using GoPress.Application.DTOs.Orders;
using GoPress.Application.Features.Orders.CreateOrder.Command;
using GoPress.Application.Features.Orders.GetAvailableOrders.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoPress.Api.Controllers.Customer
{
    [Route("api/Customers/orders")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class CustomerOrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomerOrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

       
        [HttpGet]
        public async Task<IActionResult> GetCustomerAllOrders()
        {
            var CustomerId = User.GetCurrentUser();

            var query = new GetCustomerOrdersQuery
            {
                CustomerId = CustomerId.UserId
            };

            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("shop-owner/{shopOwnerId}/price-list")]
        public async Task<IActionResult> GetShopOwnerPriceList(int shopOwnerId)
        {
            var currentUser= User.GetCurrentUser();

            var query = new GetShopOwnerPriceListQuery
            {
                ShopOwnerId = shopOwnerId
                
            };
            var response=await _mediator.Send(query);

            return Ok(response);

        }


        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderRequestDto requestDto)
        {
            var currentUser = User.GetCurrentUser();
            if (currentUser == null)
            {
                return Unauthorized();
            }
            var command = new CreateOrderCommand
            {
                CustomerId = currentUser.UserId,
                Order = requestDto
            };

            var response = await _mediator.Send(command);
            return Ok(response);
        }


      
        [HttpPut("{orderId}")]
        public async Task<IActionResult> UpdateOrder(int orderId, UpdateCustomerOrderDto dto)
        {
            var currentUser = User.GetCurrentUser();

            if (currentUser == null)
            {
                return Unauthorized();
            }
            var command =
                new UpdateCustomerOrderCommand
                {
                    OrderId = orderId,
                    CustomerId = currentUser.UserId,
                    Order = dto
                };
            var response =
                await _mediator.Send(command);

            return Ok(response);
        }

    
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var currentUser = User.GetCurrentUser();

            if (currentUser == null)
            {
                return Unauthorized();
            }
            var command = new DeleteOrderCommand
            {
                OrderId = id,
                CustomerId = currentUser.UserId
            };

            var response =
                await _mediator.Send(command);

            return Ok(response);

        }



    }
}
