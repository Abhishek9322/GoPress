using GoPress.Application.Features.Auth.Commands;
using GoPress.Application.Features.Auth.Responses;
using GoPress.Application.Interfaces.Repositories;
using GoPress.Application.Interfaces.Services;
using GoPress.Domain.Entities;
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
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        public LoginCommandHandler(IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IJwtService jwtService,
            IRefreshTokenRepository refreshTokenRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
            _refreshTokenRepository = refreshTokenRepository;
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

            var accessToken = _jwtService.GenerateToken(user);

            var refreshToken = _jwtService.GenerateRefreshToken();

            await _refreshTokenRepository.AddAsync(new RefreshToken
            {
                Token = refreshToken,
                UserId = user.Id,
                ExpiryDate = DateTime.UtcNow.AddDays(7)
            });

            return AuthResponse.SuccessResponse(
                "Login Successful",
                accessToken,
                refreshToken,
                user.Role.ToString());
        }
    }
}
