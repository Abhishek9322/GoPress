using GoPress.Application.Features.AdminApproval.Approved.Command;
using GoPress.Application.Features.AdminApproval.GetPendingApproval.Queries;
using GoPress.Application.Features.AdminApproval.Rejectuser.Command;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoPress.Api.Controllers.Admin
{
    [Route("api/AdminApprovel")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminApprovelController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AdminApprovelController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("pending-shopowners")]
        public async Task<IActionResult> GetPendingShopOwners()
        {
            var response = await _mediator.Send(
                new GetPendingShopOwnersQuery());

            return Ok(response);
        }

        [HttpGet("pending-deliveryboys")]
        public async Task<IActionResult> GetPendingDeliveryBoys()
        {
            var response = await _mediator.Send(
                new GetPendingDeliveryBoysQuery());

            return Ok(response);
        }


        [HttpPut("{userId}/approve")]
        public async Task<IActionResult> ApproveUser(int userId)
        {
            var command = new ApproveUserCommand
            {
                UserId = userId
            };

            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPut("{userId}/reject")]
        public async Task<IActionResult> RejectUser(int userId,RejectUserCommand command)
        {
            command.UserId = userId;

            var response =
                await _mediator.Send(command);

            return Ok(response);
        }
    }
}
