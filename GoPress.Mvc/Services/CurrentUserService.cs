using GoPress.Mvc.Models.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GoPress.Mvc.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _contextAccessor; 
        public CurrentUserService(IHttpContextAccessor contextAccessor  )
        {
            _contextAccessor = contextAccessor;
        }
        public CurrentUserViewModel? GetCurrentUser()
        {
            var token = GetToken();

            if (string.IsNullOrWhiteSpace(token))
            {
                return null;
            }

            var handler = new JwtSecurityTokenHandler();

            var jwtToken = handler.ReadJwtToken(token);

            return new CurrentUserViewModel
            {
                UserId = int.Parse(
                    jwtToken.Claims.FirstOrDefault(
                        x => x.Type == ClaimTypes.NameIdentifier)?.Value ?? "0"),

                FullName =
                    jwtToken.Claims.FirstOrDefault(
                        x => x.Type == ClaimTypes.Name)?.Value ?? string.Empty,

                Email =
                    jwtToken.Claims.FirstOrDefault(
                        x => x.Type == ClaimTypes.Email)?.Value ?? string.Empty,

                Role =
                    jwtToken.Claims.FirstOrDefault(
                        x => x.Type == ClaimTypes.Role)?.Value ?? string.Empty,

                Token = token
            };
        
        }

        public string? GetToken()
        {
            return _contextAccessor
                 .HttpContext?
                 .Request
                 .Cookies["AuthToken"];
        }

        public bool IsAuthenticated()
        {
            return !string.IsNullOrWhiteSpace(GetToken());
        }
    }
}
