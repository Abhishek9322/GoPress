using System.Security.Claims;

namespace GoPress.Api.Extensions
{
    public static class UserClaimsPrincipalExtensions
    {
        public static CurrentUser GetCurrentUser(
              this ClaimsPrincipal user)
        {
            var userIdClaim =
                user.FindFirst(ClaimTypes.NameIdentifier);

            var emailClaim =
                user.FindFirst(ClaimTypes.Email);

            var roleClaim =
                user.FindFirst(ClaimTypes.Role);

            if (userIdClaim == null)
            {
                throw new UnauthorizedAccessException(
                    "User Id Not Found");
            }

            if (emailClaim == null)
            {
                throw new UnauthorizedAccessException(
                    "Email Not Found");
            }

            if (roleClaim == null)
            {
                throw new UnauthorizedAccessException(
                    "Role Not Found");
            }

            return new CurrentUser
            {
                UserId = int.Parse(userIdClaim.Value),
                Email = emailClaim.Value,
                Role = roleClaim.Value
            };
        }
    }
}
