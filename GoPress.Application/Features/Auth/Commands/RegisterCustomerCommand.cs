using GoPress.Application.DTOs.Auth;
using GoPress.Application.Features.Auth.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Auth.Commands
{
    public class RegisterCustomerCommand : IRequest<AuthResponse>
    {
        public RegisterCustomerDto RegisterCustomerDto { get; set; }
    }

}
