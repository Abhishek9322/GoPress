using GoPress.Application.DTOs.Profile;
using GoPress.Application.Features.Orders.Responses;
using GoPress.Application.Features.Profile.GetProfile.Queries;
using GoPress.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Profile.GetProfile.Querieshandler
{
    public class GetDeliveryBoyProfileQueryHandler : IRequestHandler<GetDeliveryBoyProfileQuery, Response<DeliveryBoyProfileDto>>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly ILogger<GetDeliveryBoyProfileQueryHandler> _logger;
        public GetDeliveryBoyProfileQueryHandler(ILogger<GetDeliveryBoyProfileQueryHandler> logger,
            IProfileRepository profileRepository)
        {
            _logger= logger;   
            _profileRepository= profileRepository;
        }
        public async Task<Response<DeliveryBoyProfileDto>> Handle(GetDeliveryBoyProfileQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Getting DeliveryBoy Profile{UserId}"
                ,request.userId);

            var deliveryBoyProfile = await _profileRepository.GetDeliveryBoyProfileAsync(request.userId);
            if (deliveryBoyProfile == null)
            {
                return new Response<DeliveryBoyProfileDto>("DeliveryBoy Not Found");
            }

            var response = new DeliveryBoyProfileDto
            {
                FullName = deliveryBoyProfile.FullName,
                Email = deliveryBoyProfile.Email,
                PhoneNumber = deliveryBoyProfile.PhoneNumber,
                Address=deliveryBoyProfile.DeliveryBoyProfile.Address,
                City=deliveryBoyProfile.DeliveryBoyProfile.City,
                State = deliveryBoyProfile.DeliveryBoyProfile.State,
                Pincode=deliveryBoyProfile.DeliveryBoyProfile.Pincode,
                BikeNumber=deliveryBoyProfile.DeliveryBoyProfile.BikeNumber,
                AadhaarNumber=deliveryBoyProfile.DeliveryBoyProfile.AadhaarNumber,
                LicenseNumber = deliveryBoyProfile.DeliveryBoyProfile.LicenseNumber
            };

            _logger.LogInformation(
                "DeliveryBoy Profile Fetched Successfully {UserId}"
                , request.userId);

            return new Response<DeliveryBoyProfileDto>(response, "Profile Retrieved Successfully");
        }
    }
}
