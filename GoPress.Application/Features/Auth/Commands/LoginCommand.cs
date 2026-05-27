using GoPress.Application.DTOs.Auth;
using GoPress.Application.Features.Auth.Responses;
using MediatR;

namespace GoPress.Application.Features.Auth.Commands
{
    public class LoginCommand:IRequest<AuthResponse>
    {
        public LoginDto LoginDto {  get; set; }
    }
}
