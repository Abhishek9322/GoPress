using GoPress.Application.DTOs.Auth;
using GoPress.Application.Features.Auth.Responses;
using MediatR;

namespace GoPress.Application.Features.Auth.Commands
{
    public class RegisterDeliveryBoyCommand:IRequest<AuthResponse>
    {
        public RegisterDeliveryBoyDto RegisterDeliveryBoyDto { get; set; }
    }
}
