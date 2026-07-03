using GoPress.Application.DTOs.Profile;
using GoPress.Application.Features.Orders.Responses;
using GoPress.Application.Features.Profile.UpdateProfile.Command;
using GoPress.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Profile.UpdateProfile.Commandhandler
{
    public class UpdateCustomerProfileCommadhandler : IRequestHandler<UpdateCustomerProfileCommad, Response<UpdateCustomerProfileDto>>
    {
        private readonly ILogger<UpdateCustomerProfileCommadhandler> _logger;
        private readonly IProfileRepository _profileRepository;
        public UpdateCustomerProfileCommadhandler(ILogger<UpdateCustomerProfileCommadhandler> logger,
            IProfileRepository profileRepository)
        {
            _logger = logger;
            _profileRepository=profileRepository;
        }
        public async Task<Response<UpdateCustomerProfileDto>> Handle(UpdateCustomerProfileCommad request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Updating profile for User {UserId}",
                request.UserId);

            var updatedProfile =await _profileRepository.GetByIdAsync(request.UserId);
            if (updatedProfile == null)
            {
                _logger.LogWarning(
                    "User {UserId} not found",
                    request.UserId);

             return new Response<UpdateCustomerProfileDto>(
                    "User not found.");
            }
            updatedProfile.FullName = request.Profile.FullName;
            updatedProfile.PhoneNumber = request.Profile.PhoneNumber;

            updatedProfile.UpdatedAt = DateTime.UtcNow;

            await _profileRepository.updateAsync(updatedProfile);

            _logger.LogInformation(
              "Profile updated successfully for User {UserId}",
              request.UserId);

            return new Response<UpdateCustomerProfileDto>(
                "Profile updated successfully.");


        }
    }
}
