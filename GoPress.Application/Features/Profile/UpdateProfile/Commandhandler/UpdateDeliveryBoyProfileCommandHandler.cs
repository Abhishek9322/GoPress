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
    public class UpdateDeliveryBoyProfileCommandHandler : IRequestHandler<UpdateDeliveryBoyProfileCommand, Response<string>>
    {
        private readonly ILogger<UpdateDeliveryBoyProfileCommandHandler> _logger;
        private readonly IProfileRepository _profileRepository;
        public UpdateDeliveryBoyProfileCommandHandler(ILogger<UpdateDeliveryBoyProfileCommandHandler> logger,
            IProfileRepository profileRepository)
        {
            _logger = logger;
            _profileRepository = profileRepository;
        }
        public async Task<Response<string>> Handle(UpdateDeliveryBoyProfileCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Updating profile for User {UserId}",
                request.userId);

            var deliveryBoyProfile = await _profileRepository.GetDeliveryBoyProfileAsync(request.userId);
            if (deliveryBoyProfile == null)
            {
                return new Response<string>("Delivery Boy not found");
            }

            deliveryBoyProfile.FullName = request.UpdateDeliveryBoy.FullName;
            deliveryBoyProfile.PhoneNumber = request.UpdateDeliveryBoy.PhoneNumber;

            deliveryBoyProfile.DeliveryBoyProfile.Address = request.UpdateDeliveryBoy.Address;
            deliveryBoyProfile.DeliveryBoyProfile.City = request.UpdateDeliveryBoy.City;
            deliveryBoyProfile.DeliveryBoyProfile.State = request.UpdateDeliveryBoy.State;
            deliveryBoyProfile.DeliveryBoyProfile.Pincode = request.UpdateDeliveryBoy.Pincode;
            deliveryBoyProfile.DeliveryBoyProfile.BikeNumber = request.UpdateDeliveryBoy.BikeNumber;
            deliveryBoyProfile.DeliveryBoyProfile.LicenseNumber = request.UpdateDeliveryBoy.BikeNumber;
            deliveryBoyProfile.DeliveryBoyProfile.Address = request.UpdateDeliveryBoy.AadhaarNumber;



            await _profileRepository.updateAsync(deliveryBoyProfile);

            _logger.LogInformation(
                "Shop Owner {UserId} updated profile",
                request.userId);

            return new Response<string>("Profile Updated Successfully");
        }
    }
}
