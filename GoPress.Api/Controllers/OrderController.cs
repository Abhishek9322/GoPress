using GoPress.Api.Extensions;
using GoPress.Application.DTOs.Orders;
using GoPress.Application.Features.Orders.Commands;
using GoPress.Application.Features.Orders.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GoPress.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //Create Update Delete Oprations here       

        [Authorize(Roles = "Customer")]
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

        [Authorize(Roles = "Customer")]
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


        [Authorize(Roles = "Customer")]
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
         
        //Get Opration here
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var query = new GetOrderByIdQuery
            {
                OrderId = id
            };

            var response =
                await _mediator.Send(query);

            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("All-Orders")]
        public async Task<IActionResult> GetAllOrders()
        {
            // var currentCustomer = User.GetCurrentUser();

            var query = new GetAllOrdersQuery();

            var response =
                await _mediator.Send(query);
            return Ok(response);

        }

        [Authorize(Roles = "Customer")]
        [HttpGet("Customer-Orders")]
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

        [Authorize(Roles = "DeliveryBoy")]
        [HttpGet("DeliveryBoy-Orders")]
        public async Task<IActionResult> GetDeliveryOrders()
        {
            var deliveryBoyId = User.GetCurrentUser();

            var query = new GetDeliveryOrdersQuery
            {
                DeliveryBoyId = deliveryBoyId.UserId
            };

            var response = await _mediator.Send(query);
            return Ok(response);
        }


    }
}
