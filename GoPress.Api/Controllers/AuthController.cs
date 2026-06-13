using GoPress.Application.DTOs.Auth;
using GoPress.Application.Features.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoPress.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register-customer")]
        public async Task<IActionResult> RegisterCustomer(RegisterCustomerDto customerDto)
        {
            var command=new RegisterCustomerCommand
            {
                RegisterCustomerDto = customerDto
            };
            var result = await _mediator.Send(command);
            return Ok(result);


        }

        [HttpPost("All-Login")]
        public async Task<IActionResult> Login(LoginDto login)
        {
            var command = new LoginCommand
            {
                LoginDto = login
            };

            var result = await _mediator.Send(command);

            if (!result.Success)
            {
                return BadRequest(result);
            }
            // Store JWT in Cookie
            Response.Cookies.Append(
                "AuthToken",
                result.AccessToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddDays(1)
                });
            // Refresh Token Cookie
            Response.Cookies.Append(
                "RefreshToken",
                result.RefreshToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddDays(7)
                });


            return Ok(result);
        }


        [HttpPost("register-shop-owner")]
        public async Task<IActionResult> RegisterShopOwner(RegisterShopOwnerDto dto)
        {
            var command = new RegisterShopOwnerCommand
            {
                RegisterShopOwnerDto = dto
            };

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPost("register-Delivery-Boy")]
        public async Task<IActionResult> RegisterDeliveryBoy(RegisterDeliveryBoyDto dto)
        {
            var command = new RegisterDeliveryBoyCommand
            {
                RegisterDeliveryBoyDto = dto
            };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequestDto refreshDto)
        {
            var command = new RefreshTokenCommand
            {
                RefreshTokenDto = refreshDto
            };

            var result =
                await _mediator.Send(command);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        //just for the test here
        [Authorize(Roles = "Customer")]
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            return Ok("Authenticated User");
        }
    }
}
