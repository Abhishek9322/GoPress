using GoPress.Application.Features.Auth.Commands;
using GoPress.Application.Features.Auth.Responses;
using GoPress.Application.Interfaces.Repositories;
using GoPress.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Auth.CommandHndler
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;
        public LoginCommandHandler(IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IJwtService jwtService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }
        public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var dto = request.LoginDto;

            var user = await _userRepository
                .GetUserByEmailAsync(dto.Email);

            if (user == null)
            {
                return AuthResponse.FailureResponse(
                    "Invalid email or password.");
            }

            var isPasswordValid = _passwordHasher
                .VerifyPassword(dto.Password, user.PasswordHash);

            if (!isPasswordValid)
            {
                return AuthResponse.FailureResponse(
                    "Invalid Password");
            }

            if (!user.IsApproved)
            {
                return AuthResponse.FailureResponse(
                    "Your account is pending admin approval.");
            }

            if (!user.IsActive)
            {
                return AuthResponse.FailureResponse(
                    "Your account is inactive.");
            }

            var token = _jwtService.GenerateToken(user);

            return AuthResponse.SuccessResponse(
                "Login Successful",
                token,
                user.Role.ToString());
        }
    }
}
