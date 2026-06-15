using GoPress.Api.Extensions;
using GoPress.Application.Features.Orders.AcceptOrder.Comman;
using GoPress.Application.Features.Orders.AcceptOrder.Command;
using GoPress.Application.Features.Orders.GetAvailableOrders.Queries;
using GoPress.Application.Features.Orders.ProcessingOrder.Command;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoPress.Api.Controllers.DeliveryBoy
{
    [ApiController]
    [Route("api/delivery-boys/orders")]
    [Authorize(Roles = "DeliveryBoy")]
    public class DeliveryBoyOrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DeliveryBoyOrdersController(IMediator mediator)
        {
             _mediator = mediator;
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableOrders()
        {
            var query = new GetAvailableDBoyOrdersQuery();

            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPut("{orderId}/accept")]
        public async Task<IActionResult> AcceptPickup(int orderId)
        {
            var currentUser = User.GetCurrentUser();

            var command = new AcceptPickupByDBoyCommand
            {
                OrderId = orderId,
                DeliveryBoyId = currentUser.UserId
            };

            var response = await _mediator.Send(command);

            return Ok(response);
        }

        
        [HttpGet]     //Extra thing will mpdify  to see order histry of the dboy here
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


        [HttpPut("{orderId}/pickup-completed")]
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

        [HttpGet("ready-for-delivery")]
        public async Task<IActionResult> GetReadyForDeliveryOrders()
        {
            var currentUser = User.GetCurrentUser();

            var query = new GetReadyForDeliveryOrdersQuery
            {
                DeliveryBoyId =
                        currentUser.UserId
            };

            var response = await _mediator.Send(query);

            return Ok(response);
        }

        [HttpPut("{orderId}/start-delivery")]
        public async Task<IActionResult> StartDelivery(int orderId)
        {
            var currentUser = User.GetCurrentUser();

            var command = new StartDeliveryCommand
            {
                OrderId = orderId,
                DeliveryBoyId =
                        currentUser.UserId
            };

            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPut("{orderId}/deliver")]
        public async Task<IActionResult> DeliverOrder(int orderId)
        {
            var currentUser = User.GetCurrentUser();

            var command = new DeliverOrderCommand
            {
                OrderId = orderId,
                DeliveryBoyId =
                        currentUser.UserId
            };

            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpGet("delivered")]
        public async Task<IActionResult>GetDeliveredOrders()
        {
            var currentUser =User.GetCurrentUser();

            var query =new GetComplitedOrderByDBoyQuery
            {
                    DeliveryBoyId = currentUser.UserId
                };

            var response =await _mediator.Send(query);

            return Ok(response);
        }
    }
}
