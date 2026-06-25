using GoPress.Application.Features.Auth.Commands;
using GoPress.Application.Features.Auth.Responses;
using GoPress.Application.Interfaces.Repositories;
using GoPress.Application.Interfaces.Services;
using GoPress.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<LoginCommandHandler> _logger;
        public LoginCommandHandler(IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IJwtService jwtService,
            IRefreshTokenRepository refreshTokenRepository,
            ILogger<LoginCommandHandler> logger)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
            _refreshTokenRepository = refreshTokenRepository;
            _logger = logger;
        }
        public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var dto = request.LoginDto;

            _logger.LogInformation(
                   "Login attempt for email {Email}",
               dto.Email);

            var user = await _userRepository
                .GetUserByEmailAsync(dto.Email);

            if (user == null)
            {
                _logger.LogWarning(
                   "Login failed. User not found for email {Email}",
                   dto.Email);

                return AuthResponse.FailureResponse(
                    "Invalid email or password.");
            }

            var isPasswordValid = _passwordHasher
                .VerifyPassword(dto.Password, user.PasswordHash);

            if (!isPasswordValid)
            {
                _logger.LogWarning(
                    "Login failed. Invalid password for user {Email}",
                   dto.Email);

                return AuthResponse.FailureResponse(
                    "Invalid Password");
            }

            if (!user.IsApproved)
            {
                 _logger.LogWarning(
                  "Login failed. User {UserId} is not approved",
                    user.Id);

                return AuthResponse.FailureResponse(
                    "Your account is pending admin approval.");
            }

            if (!user.IsActive)
            {
                _logger.LogWarning(
                    "Login failed. User {UserId} account is inactive",
                    user.Id);

                return AuthResponse.FailureResponse(
                    "Your account is inactive.");
            }

            _logger.LogInformation(
                "Generating tokens for user {UserId}",
                user.Id);

            var accessToken = _jwtService.GenerateToken(user);

            var refreshToken = _jwtService.GenerateRefreshToken();

            _logger.LogInformation(
             "User {UserId} logged in successfully with role {Role}",
                   user.Id,
                  user.Role);

            await _refreshTokenRepository.AddAsync(new RefreshToken
            {
                Token = refreshToken,
                UserId = user.Id,
                ExpiryDate = DateTime.UtcNow.AddDays(7)
            });

            _logger.LogInformation(
                "User Login Successfully  {UserId}",
                user.Id);

            return AuthResponse.SuccessResponse(
                "Login Successful",
                accessToken,
                refreshToken,
                user.Role.ToString());
        }
    }
}
