using GoPress.Application.Features.Orders.Responses;
using GoPress.Application.Features.ShopOpration.UpdateShopTime.Command;
using GoPress.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.ShopOpration.UpdateShopTime.Commandhandler
{
    public class UpdateShopTimeCommandHandler : IRequestHandler<UpdateShopTimeCommand, Response<string>>
    {
        private readonly ILogger<UpdateShopTimeCommandHandler> _logger;
        private readonly IUserRepository _userRepository;
        public UpdateShopTimeCommandHandler(ILogger<UpdateShopTimeCommandHandler> logger,IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }
        public async Task<Response<string>> Handle(UpdateShopTimeCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                  "Updating timings for ShopOwner {ShopOwnerId}",
                  request.ShopOwnerId);


            var profile = await _userRepository
               .GetShopOwnerProfileByUserIdAsync(request.ShopOwnerId);

            if (profile == null)
            {
                _logger.LogWarning(
                    "Shop profile not found for {ShopOwnerId}",
                    request.ShopOwnerId);

                return new Response<string>("Shop profile not found.");
            }

            if (request.Timing.OpningTime >= request.Timing.ClosingTime)
            {
                return new Response<string>(
                    "Opening Time must be earlier than Closing Time.");
            }

            profile.OpeningTime = request.Timing.OpningTime;
            profile.ClosingTime = request.Timing.ClosingTime;

            await _userRepository.UpdateShopStatus(profile);

            _logger.LogInformation(
                "Shop timings updated successfully for {ShopOwnerId}",
                request.ShopOwnerId);

            return new Response<string>(
                "Shop timings updated successfully.");
        }
    }
}
