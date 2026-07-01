using GoPress.Application.Features.AdminOrdersmanagment.GetAllOrderByStatus.Queries;
using GoPress.Application.Features.Orders.GetAvailableOrders.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        //[HttpGet("All-Orders")]
        //public async Task<IActionResult> GetAllOrders()
        //{
        //    // var currentCustomer = User.GetCurrentUser();

        //    var query = new GetAllOrdersQuery();

        //    var response =
        //        await _mediator.Send(query);
        //    return Ok(response);

        //}
        ////Get Opration here
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetOrderById(int id)
        //{
        //    var query = new GetOrderByIdQuery
        //    {
        //        OrderId = id
        //    };

        //    var response =
        //        await _mediator.Send(query);

        //    return Ok(response);
        //}



        [HttpGet("Pending-Orders")]
        public async Task<IActionResult> GetPendingOrders()
        {
            var response = await _mediator.Send(
                new GetPendingOrdersByAdminQuery());

            return Ok(response);

        }


        [HttpGet("Deliverd-Orders")]
        public async Task<IActionResult> GetDeliverdOrders()
        {
            var response=await _mediator.Send(
                new GetdeliverdOrderByAdminQuery());

            return Ok(response);
        }


    }
}
