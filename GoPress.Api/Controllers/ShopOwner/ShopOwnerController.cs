using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoPress.Api.Controllers.ShopOwner
{
    [Route("api/ShopOwner")]
    [ApiController]
    [Authorize(Roles = "ShopOwner")]
    public class ShopOwnerController : ControllerBase
    {

        //Dashboard and all thing here

        
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            return Ok("Authenticated User ShopOwner");
        }
    }
}
