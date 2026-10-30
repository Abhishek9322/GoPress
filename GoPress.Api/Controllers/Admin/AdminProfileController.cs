using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoPress.Api.Controllers.Admin
{
    [Route("api/Admin/Profiles")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminProfileController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AdminProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("Get-Profile")]
        public async Task<IActionResult> GetProfile()
        {
            return Ok();
        }
    }

}
