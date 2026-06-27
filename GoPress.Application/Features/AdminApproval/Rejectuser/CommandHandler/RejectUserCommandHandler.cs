using GoPress.Application.Features.AdminApproval.Rejectuser.Command;
using GoPress.Application.Features.Orders.Responses;
using GoPress.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.AdminApproval.Rejectuser.CommandHandler
{
    public class RejectUserCommandHandler : IRequestHandler<RejectUserCommand, Response<string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<RejectUserCommandHandler> _logger;
        public RejectUserCommandHandler(IUserRepository userRepository,ILogger<RejectUserCommandHandler> logger)
        {
            _userRepository=userRepository;
            _logger = logger;
        }
        public async Task<Response<string>> Handle(RejectUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
               "Admin is rejecting user {UserId}",
               request.UserId);

            var user = await _userRepository.GetByIdAsync(request.UserId);

            if (user == null)
            {
                _logger.LogWarning(
                    "User {UserId} not found.",
                    request.UserId);

                return new Response<string>(
                    "User not found.");
            }

            //if (user.IsApproved)
            //{
            //    return new Response<string>(
            //        "Approved users cannot be rejected.");
            //}

            if (string.IsNullOrWhiteSpace(request.Reason))
            {
                return new Response<string>(
                    "Rejection reason is required.");
            }

            user.IsApproved = false;
            user.IsActive = false;

            user.RejectionReason = request.Reason;
            user.RejectedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;

            await _userRepository.SaveChangesAsync();

            _logger.LogInformation(
              "User {UserId} rejected successfully.",
              user.Id);

            return new Response<string>(
              "User rejected successfully.");

        }
    }
}
