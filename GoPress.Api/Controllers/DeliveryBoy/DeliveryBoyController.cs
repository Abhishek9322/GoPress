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

namespace GoPress.Api.Controllers.DeliveryBoy
{
    [ApiController]
    [Route("api/delivery-boys")]
    [Authorize(Roles = "DeliveryBoy")]
    public class DeliveryBoyController : ControllerBase       
    {

        //Dashboard and all thing here
        private readonly IMediator _mediator;
        public DeliveryBoyController(IMediator mediator)
        {
            _mediator = mediator;
        }

       
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            return Ok("Authenticated User DeliveryBoy");
        }


    }
}
