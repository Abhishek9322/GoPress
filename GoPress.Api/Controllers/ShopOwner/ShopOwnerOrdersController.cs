using GoPress.Api.Extensions;
using GoPress.Application.Features.Orders.AcceptOrder.Comman;
using GoPress.Application.Features.Orders.GetAvailableOrders.Queries;
using GoPress.Application.Features.Orders.ProcessingOrder.Command;
using GoPress.Application.Features.Orders.RejectOrder.command;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoPress.Api.Controllers.ShopOwner
{
    [Route("api/ShopOwner/Orders")]
    [ApiController]
    [Authorize(Roles = "ShopOwner")]
    public class ShopOwnerOrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ShopOwnerOrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }


       
        [HttpPut("{orderId}/accept-order")]
        public async Task<IActionResult> AcceptOrder(int orderId)
        {
            var currentUser = User.GetCurrentUser();
            if (currentUser == null)
            {
                return Unauthorized();
            }
            var command = new AcceptOrderByShopOwnerCommand
            {
                OrderId = orderId,
                ShopOwnerId = currentUser.UserId
            };

            var response = await _mediator.Send(command);
            return Ok(response);

        }


  
        [HttpPut("{orderId}/reject-order")]
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


        //
        [HttpGet("All-Rejected-Orders")]
        public async Task<IActionResult> GetRejectOrder()
        {
           var currentUser=User.GetCurrentUser();

            var result = new GetRejectOrderShopOwnerQuery
            {
                ShopOwnerId = currentUser.UserId,
            };

            var response = await _mediator.Send(result);

            return Ok(response);
        }


        [HttpGet("all-orders")]
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


        [HttpPut("{orderId}/start-processing")]
        public async Task<IActionResult> StartProcessing(int orderId)
        {
            var currentUser = User.GetCurrentUser();

            var command = new ShopOwnerStartProcessingCommand
            {
                OrderId = orderId,
                ShopOwnerId = currentUser.UserId
            };

            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPut("{orderId}/ready-for-delivery")]
        public async Task<IActionResult> ReadyForDelivery(int orderId)
        {
            var currentUser = User.GetCurrentUser();

            var command = new ShopOwnerReadyForDeliveryCommand
            {
                OrderId = orderId,
                ShopOwnerId = currentUser.UserId
            };

            var response = await _mediator.Send(command);

            return Ok(response);
        }

        //
        [HttpGet("All-ReadyforDelivery-Order")]
        public async Task<IActionResult> GetReadyForDelivery()
        {
            var currentUser = User.GetCurrentUser();

            var result = new GetReadyForDeliveryByShopOwnerQuery
            {
                ShopOwnerId = currentUser.UserId
            };

            var response = await _mediator.Send(result);

            return Ok(response);
        }




        //
        [HttpGet("completed-orders")]
         public async Task<IActionResult> GetCompletedOrders()
        {
            var currentuser=User.GetCurrentUser();

            var response = new GetCompletedOrdersByShopOwnerQuery
            {
                ShopOwnerId = currentuser.UserId
            };

            var result=await _mediator.Send(response);

            return Ok(result);
        }


    }


}
