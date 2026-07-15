namespace GoPress.Mvc.Services
{
    public class TokenService : ITokenService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public TokenService(IHttpContextAccessor contextAccessor    )
        {
            _contextAccessor = contextAccessor;
        }
      

        public void SaveToken(string token)
        {
            _contextAccessor.HttpContext.Response.Cookies.Append(
             "AuthToken",
             token,
             new CookieOptions
             {
                 HttpOnly = true,
                 Secure = true,
                 SameSite = SameSiteMode.Strict,
                 Expires = DateTimeOffset.UtcNow.AddDays(1)
             });
        }

        public string? GetToken()
        {
           return _contextAccessor.HttpContext?.Request.Cookies["AuthToken"];

        }
        public void RemoveToken()
        {
            _contextAccessor.HttpContext?.Response.Cookies.Delete("AuthToken");
        }

      
    }
}
