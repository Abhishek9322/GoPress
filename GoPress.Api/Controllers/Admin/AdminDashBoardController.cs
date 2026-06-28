using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoPress.Api.Controllers.Admin
{
    [Route("api/Admin/Dashboard")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminDashBoardController : ControllerBase
    {
          //Dashboard and all thing here
     
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            return Ok("Authenticated User Admin");
        }
    }
}
