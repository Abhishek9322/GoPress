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
    }
}
