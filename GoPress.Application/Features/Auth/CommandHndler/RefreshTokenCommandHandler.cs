using GoPress.Application.Features.Auth.Commands;
using GoPress.Application.Features.Auth.Responses;
using GoPress.Application.Features.Orders.Responses;
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
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResponse>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public RefreshTokenCommandHandler(
            IRefreshTokenRepository refreshTokenRepository,
            IUserRepository userRepository,
            IJwtService jwtService)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
            _jwtService = jwtService;
        }
        public async Task<AuthResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var storedToken =
           await _refreshTokenRepository
           .GetByTokenAsync(
               request.RefreshTokenDto.RefreshToken);

            if (storedToken == null)
            {
                return AuthResponse.FailureResponse(
                    "Invalid Refresh Token");
            }

            if (storedToken.IsRevoked)
            {
                return AuthResponse.FailureResponse(
                    "Refresh Token Revoked");
            }

            if (storedToken.ExpiryDate < DateTime.UtcNow)
            {
                return AuthResponse.FailureResponse(
                    "Refresh Token Expired");
            }

            var user =
                await _userRepository
                .GetByIdAsync(storedToken.UserId);

            if (user == null)
            {
                return AuthResponse.FailureResponse(
                    "User Not Found");
            }

            var newAccessToken =
                _jwtService.GenerateToken(user);

            var newRefreshToken =
                _jwtService.GenerateRefreshToken();

            storedToken.IsRevoked = true;

            await _refreshTokenRepository.UpdateAsync(
                storedToken);

            await _refreshTokenRepository.AddAsync(
                new RefreshToken
                {
                    UserId = user.Id,
                    Token = newRefreshToken,
                    ExpiryDate = DateTime.UtcNow.AddDays(7)
                });

            return AuthResponse.SuccessResponse(
                "Token Refreshed",
                newAccessToken,
                newRefreshToken,
                user.Role.ToString());
        }
    }
}
