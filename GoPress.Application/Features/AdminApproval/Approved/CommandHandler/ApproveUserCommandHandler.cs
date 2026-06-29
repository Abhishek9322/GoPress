using GoPress.Application.Comman.Caching;
using GoPress.Application.Features.AdminApproval.Approved.Command;
using GoPress.Application.Features.Orders.Responses;
using GoPress.Application.Interfaces.Caching;
using GoPress.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.AdminApproval.Approved.CommandHandler
{
    public class ApproveUserCommandHandler : IRequestHandler<ApproveUserCommand, Response<string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<ApproveUserCommandHandler> _logger;
        private readonly ICacheService _cacheService;
        public ApproveUserCommandHandler(IUserRepository userRepository,
            ILogger<ApproveUserCommandHandler> logger,
            ICacheService cacheService)
        {
            _userRepository = userRepository;
            _logger = logger;
            _cacheService = cacheService;
        }
        public async Task<Response<string>> Handle(ApproveUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Admin is approving user {UserId}",
                request.UserId);

            var user =await _userRepository.GetByIdAsync(request.UserId);

            if (user == null)
            {
                _logger.LogWarning(
                    "User {UserId} not found",
                    request.UserId);

                return new Response<string>("User not found.");
            }

            if (user.IsApproved)
            {
                return new Response<string>(
                    "User is already approved.");
            }


            user.IsApproved = true;
            user.IsActive = true;
            user.UpdatedAt = DateTime.UtcNow;

            await _userRepository.UpdateAsync(user);

            await _cacheService.RemoveAsync(CacheKeys.AdminDashboard);

            _logger.LogInformation(
                "User {UserId} approved successfully.",
                user.Id);

            return new Response<string>(
                "User approved successfully.");
        }
    }
}
