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
        public string Token { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;
        // Success Response
        public static AuthResponse SuccessResponse(
            string message,
            string token = "",
            string role = "")
        {
            return new AuthResponse
            {
                Success = true,
                Message = message,
                Token = token,
                Role = role
            };
        }

        // Failure Response
        public static AuthResponse FailureResponse(
            string message)
        {
            return new AuthResponse
            {
                Success = false,
                Message = message
            };
        }
    }
}
