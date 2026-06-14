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
    }
}
