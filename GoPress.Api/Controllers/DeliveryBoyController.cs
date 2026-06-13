using GoPress.Api.Extensions;
using GoPress.Application.Features.Orders.AcceptOrder.Comman;
using GoPress.Application.Features.Orders.RejectOrder.command;
using GoPress.Application.Features.Orders.CreateOrder.Command;
using GoPress.Application.Features.Orders.GetAvailableOrders.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GoPress.Application.Features.Orders.AcceptOrder.Command;
using GoPress.Application.Features.Orders.ProcessingOrder.Command;

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
            var query = new GetAvailableDBoyOrdersQuery();  

            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPut("accept-pickup/{orderId}")]
        public async Task<IActionResult> AcceptPickup(int orderId)
        {
            var currentUser = User.GetCurrentUser();

            var command = new AcceptPickupByDBoyCommand
                {
                    OrderId = orderId,
                    DeliveryBoyId = currentUser.UserId
                };

            var response =await _mediator.Send(command);

            return Ok(response);
        }

        [Authorize(Roles = "DeliveryBoy")]
        [HttpGet("DeliveryBoy-Orders")]     //Extra thing will mpdify  to see order histry of the dboy here
        public async Task<IActionResult> GetDeliveryOrders()
        {
            var deliveryBoyId = User.GetCurrentUser();

            var query = new GetDeliveryDBoyOrdersQuery
            {
                DeliveryBoyId = deliveryBoyId.UserId
            };

            var response = await _mediator.Send(query);
            return Ok(response);
        }


        [HttpPut("pickup-completed/{orderId}")]
        public async Task<IActionResult> PickupCompleted(int orderId)
        {
            var currentUser = User.GetCurrentUser();

            var order = new PickupCompletedCommand
            {
                OrderId = orderId,
                DeliveryBoyId = currentUser.UserId
            };

            var response = await _mediator.Send(order);

            return Ok(response);
        }

        [HttpGet("ready-for-delivery-orders")]
        public async Task<IActionResult> GetReadyForDeliveryOrders()
        {
            var currentUser = User.GetCurrentUser();

            var query =new GetReadyForDeliveryOrdersQuery
                {
                    DeliveryBoyId =
                        currentUser.UserId
                };

            var response = await _mediator.Send(query);

            return Ok(response);
        }

        [HttpPut("start-delivery/{orderId}")]
        public async Task<IActionResult> StartDelivery(int orderId)
        {
            var currentUser = User.GetCurrentUser();

            var command =  new StartDeliveryCommand
                {
                    OrderId = orderId,
                    DeliveryBoyId =
                        currentUser.UserId
                };

            var response =await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPut("deliver-order/{orderId}")]
        public async Task<IActionResult>DeliverOrder(int orderId)
        {
            var currentUser =User.GetCurrentUser();

            var command = new DeliverOrderCommand
                {
                    OrderId = orderId,
                    DeliveryBoyId =
                        currentUser.UserId
                };

            var response =await _mediator.Send(command);

            return Ok(response);
        }
    }
}
