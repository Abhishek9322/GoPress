using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoPress.Api.Controllers.Customer
{
    [Route("api/Customers/Home")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class CustomerHomeController : ControllerBase
    {

        //Dashboard and all thing here

        
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            return Ok("Authenticated User Customer");
        }
    }

}
