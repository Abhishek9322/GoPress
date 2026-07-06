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
using GoPress.Application.Features.DashBoard.Queries;

namespace GoPress.Api.Controllers.DeliveryBoy
{
    [ApiController]
    [Route("api/delivery-boys/Dashboard")]
    [Authorize(Roles = "DeliveryBoy")]
    public class DeliveryBoyDashboardController : ControllerBase       
    {
        private readonly IMediator _mediator;
        public DeliveryBoyDashboardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
       public async Task<IActionResult> DeliveryBoyDashboard()
       {
            var currentUser = User.GetCurrentUser();

            var response=await _mediator.Send(new GetDeliveryBoyDashboardQuery
            {
                DeliveryBoyId = currentUser.UserId
            });

            return Ok(response);
        }
    }
}
