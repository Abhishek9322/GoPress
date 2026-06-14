using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoPress.Api.Controllers.Admin
{
    [Route("api/Admin")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
          //Dashboard and all thing here
     
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            return Ok("Authenticated User Admin");
        }
    }
}
