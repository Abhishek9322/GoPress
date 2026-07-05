using GoPress.Api.Extensions;
using GoPress.Application.DTOs.Profile;
using GoPress.Application.Features.Profile.GetProfile.Queries;
using GoPress.Application.Features.Profile.UpdateProfile.Command;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoPress.Api.Controllers.ShopOwner
{
    [Route("api/ShopOwner/Profiles")]
    [ApiController]
    [Authorize(Roles = "ShopOwner")]
    public class ShopOwnerProfileController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ShopOwnerProfileController(IMediator mediator)
        {
            _mediator=mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            var currentUser = User.GetCurrentUser();

            var response = await _mediator.Send(new GetShopOwnerProfileQuery
            {
                userId = currentUser.UserId
            });

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProfile(UpdateShopOwnerProfileDto updateShopOwner)
        {
            var currentUser = User.GetCurrentUser();

            var response = await _mediator.Send(new UpdateShopOwnerProfileCommand
            {
                userId = currentUser.UserId,
                UpdateShopOwnerProfile = updateShopOwner
            });

            return Ok(response);
        }
    }
}
