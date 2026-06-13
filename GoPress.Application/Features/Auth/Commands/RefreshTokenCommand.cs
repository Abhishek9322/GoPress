using GoPress.Application.DTOs.Auth;
using GoPress.Application.Features.Auth.Responses;
using GoPress.Application.Features.Orders.Responses;
using MediatR;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Auth.Commands
{
    public class RefreshTokenCommand:IRequest<AuthResponse>
    {
        public RefreshTokenRequestDto RefreshTokenDto { get; set; }
    }
}
