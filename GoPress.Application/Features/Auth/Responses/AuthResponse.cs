using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Auth.Responses
{
    public class AuthResponse
    {

        public bool Success { get; set; }

        public string Message { get; set; }

        public string AccessToken { get; set; } = string.Empty;

        public string RefreshToken { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public static AuthResponse SuccessResponse(
            string message,
            string accessToken = "",
            string refreshToken = "",
            string role = "")
        {
            return new AuthResponse
            {
                Success = true,
                Message = message,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                Role = role
            };
        }

        public static AuthResponse FailureResponse(string message)
        {
            return new AuthResponse
            {
                Success = false,
                Message = message
            };
        }
    }
}
