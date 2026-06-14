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
    }
}
