using GoPress.Application.Features.AdminOrdersmanagment.GetAllOrderByStatus.Queries;
using GoPress.Application.Features.Orders.GetAvailableOrders.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace GoPress.Api.Controllers.Admin
{
    [Route("api/Admin/Orders")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminOrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AdminOrderController(IMediator mediator)
        {
            _mediator= mediator;
        }

        [HttpGet("All-Orders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var response =await _mediator.Send(new GetAllOrdersQuery());
            return Ok(response);

        }
        ////Get Opration here
        [HttpGet("{orderId}/OrderDetails")]
        public async Task<IActionResult> GetOrderDetails(int orderId)
        {
            var query = new GetOrderByIdQuery
            {
                OrderId = orderId
            };

            var response =await _mediator.Send(query);
            return Ok(response);
        }


        [HttpGet("Pending-Orders")]
        public async Task<IActionResult> GetPendingOrders()
        {
            var response = await _mediator.Send(
                new GetPendingOrdersByAdminQuery());

            return Ok(response);

        }
        [HttpGet("Pickup-Assigned")]
        public async Task<IActionResult> GetAllPickupAssignedOrders()
        {
            var response = await _mediator.Send(new GetAllPickupAssignedOrdersQuery());   

            return Ok(response);
        }


        [HttpGet("PickedUp-Orders")]
        public async Task<IActionResult> GetAllPickedUpOrders()
        {
            var response = await _mediator.Send(new GetAllPickedUpOrdersQuery());

            return Ok(response);
        }


        [HttpGet("Processing-Orders")]
        public async Task<IActionResult> GetAllProcessingOrders()
        {
            var response = await _mediator.Send(new GetAllProcessingOrdersQuery());
            return Ok(response);
        }

        [HttpGet("ReadyForDelivery-Orders")]
        public async Task<IActionResult> GetAllReadyForDeliveryOrders()
        {

            var response = await _mediator.Send(new GetAllReadyForDeliveryOrdersQuery());
            return Ok(response);
        }



        [HttpGet("Deliverd-Orders")]
        public async Task<IActionResult> GetDeliverdOrders()
        {
            var response=await _mediator.Send(
                new GetdeliverdOrderByAdminQuery());

            return Ok(response);
        }

        [HttpGet("Rejected-Orders")]
        public async Task<IActionResult> GetAllRejectedOrders()
        {
            var response = await _mediator.Send(
                new GetAllRejectedOrderByAdminQuery());

            return Ok(response);
        }

        [HttpGet("Accepted-Orders")]
        public async Task<IActionResult> GetAllAcceptedOrders()
        {
            var response = await _mediator.Send(
                new GetAllAcceptedOrderByAdminQuery());

            return Ok(response);
        }

        [HttpGet("canceld-Orders")]
        public async Task<IActionResult> GetAllCanceledOrders()
        {
            var response = await _mediator.Send(
                new GetAllCancaledOrderByAdminQuery()); 

            return Ok(response);
        }


    }
}
