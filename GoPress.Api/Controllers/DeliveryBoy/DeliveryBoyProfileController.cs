using GoPress.Api.Extensions;
using GoPress.Application.DTOs.Profile;
using GoPress.Application.Features.Profile.GetProfile.Queries;
using GoPress.Application.Features.Profile.UpdateProfile.Command;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoPress.Api.Controllers.DeliveryBoy
{
    [Route("api/delivery-boys/Profiles")]
    [ApiController]
    [Authorize(Roles = "DeliveryBoy")]
    public class DeliveryBoyProfileController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DeliveryBoyProfileController(IMediator mediator)
        {
            _mediator=mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            var currentUser = User.GetCurrentUser();

            var response = await _mediator.Send(new GetDeliveryBoyProfileQuery
            {
                userId=currentUser.UserId
            });
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> updateProfile(UpdateDeliveryBoyProfileDto deliveryBoyProfile)
        {
            var currentUser = User.GetCurrentUser();

            var response = await _mediator.Send(new UpdateDeliveryBoyProfileCommand
            {
                userId = currentUser.UserId,
                UpdateDeliveryBoy = deliveryBoyProfile
            });


            return Ok(response);
        }
    }
}
