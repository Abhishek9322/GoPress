using GoPress.Application.Comman.Caching;
using GoPress.Application.Features.AdminApproval.ActiveDeactiveUser.Command;
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

namespace GoPress.Application.Features.AdminApproval.ActiveDeactiveUser.CommandHandler
{
    public class ActivateDeactivateUserCommandHandler : IRequestHandler<ActivateDeactivateUserCommand, Response<string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<ActivateDeactivateUserCommandHandler> _logger;
        private readonly ICacheService _cacheService;
        public ActivateDeactivateUserCommandHandler(IUserRepository userRepository,
            ILogger<ActivateDeactivateUserCommandHandler> logger,
            ICacheService cacheService)
        {
            _userRepository = userRepository;
            _logger = logger;
            _cacheService = cacheService;
        }
        public async Task<Response<string>> Handle(ActivateDeactivateUserCommand request, CancellationToken cancellationToken)
        {
            var ActiveDeactiveuser=await _userRepository.GetByIdAsync(request.UserId);

            if (ActiveDeactiveuser == null)
            {
                return new Response<string>("User not found.");
            }

            if (!ActiveDeactiveuser.IsApproved)
            {
                return new Response<string>(
                    "User is not approved.");
            }

            ActiveDeactiveuser.IsActive = request.IsActive;
            ActiveDeactiveuser.UpdatedAt = DateTime.UtcNow;

            await _userRepository.UpdateAsync(ActiveDeactiveuser);

            await _cacheService.RemoveAsync(CacheKeys.AdminDashboard);

            _logger.LogInformation(
                "Admin changed user {UserId} active status to {Status}",
                request.UserId,
                request.IsActive);

            return new Response<string>(
                request.IsActive
                    ? "User activated successfully."
                    : "User deactivated successfully.");
        }
    }
}
