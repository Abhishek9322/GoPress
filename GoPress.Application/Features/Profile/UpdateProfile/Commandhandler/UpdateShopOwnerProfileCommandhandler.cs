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
    public class UpdateShopOwnerProfileCommandhandler : IRequestHandler<UpdateShopOwnerProfileCommand, Response<string>>
    {
        private readonly ILogger<UpdateShopOwnerProfileCommandhandler> _logger;
        private readonly IProfileRepository _profileRepository;
        public UpdateShopOwnerProfileCommandhandler(ILogger<UpdateShopOwnerProfileCommandhandler> logger,
            IProfileRepository profileRepository)
        {
            _logger = logger;
            _profileRepository = profileRepository;
        }
        public async Task<Response<string>> Handle(UpdateShopOwnerProfileCommand request, CancellationToken cancellationToken)
        {
            var shopOwner = await _profileRepository.GetShopOwnerProfileAsync(request.userId);

            if (shopOwner == null)
            {
                return new Response<string>("Shop Owner Not Found");
            }

            shopOwner.FullName = request.UpdateShopOwnerProfile.FullName;
            shopOwner.PhoneNumber = request.UpdateShopOwnerProfile.PhoneNumber;

            shopOwner.ShopOwnerProfile.ShopName = request.UpdateShopOwnerProfile.ShopName;
            shopOwner.ShopOwnerProfile.ShopAddress = request.UpdateShopOwnerProfile.ShopAddress;
            shopOwner.ShopOwnerProfile.City = request.UpdateShopOwnerProfile.City;
            shopOwner.ShopOwnerProfile.State = request.UpdateShopOwnerProfile.State;
            shopOwner.ShopOwnerProfile.Pincode = request.UpdateShopOwnerProfile.Pincode;
            shopOwner.ShopOwnerProfile.ShopLicenseNumber = request.UpdateShopOwnerProfile.ShopLicenseNumber;
            shopOwner.ShopOwnerProfile.GSTNumber = request.UpdateShopOwnerProfile.GSTNumber;

            shopOwner.UpdatedAt = DateTime.UtcNow;

            await _profileRepository.updateAsync(shopOwner);

            _logger.LogInformation(
                "Shop Owner {UserId} updated profile",
                request.userId);

            return new Response<string>("Profile Updated Successfully");
        }
    }
}
