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
    public class GetShopOwnerProfileQueryHandler : IRequestHandler<GetShopOwnerProfileQuery, Response<ShopOwnerProfileDto>>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly ILogger<GetShopOwnerProfileQueryHandler> _logger;
        public GetShopOwnerProfileQueryHandler(ILogger<GetShopOwnerProfileQueryHandler> logger,
            IProfileRepository profileRepository)
        {
            _logger= logger;
            _profileRepository = profileRepository;
        }
        public async Task<Response<ShopOwnerProfileDto>> Handle(GetShopOwnerProfileQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                  "Getting shop owner profile {UserId}",
                  request.userId);

            var user =await _profileRepository.GetShopOwnerProfileAsync(request.userId);
            if (user == null)
            {
                return new Response<ShopOwnerProfileDto>("Shop Owner Not Found");
            }

            var shopOwnerProfile = new ShopOwnerProfileDto
            {
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,

                ShopName = user.ShopOwnerProfile.ShopName,
                ShopAddress = user.ShopOwnerProfile.ShopAddress,
                City = user.ShopOwnerProfile.City,
                State = user.ShopOwnerProfile.State,
                Pincode = user.ShopOwnerProfile.Pincode,
                ShopLicenseNumber = user.ShopOwnerProfile.ShopLicenseNumber,
                GSTNumber = user.ShopOwnerProfile.GSTNumber
            };

            return new Response<ShopOwnerProfileDto>(
                shopOwnerProfile, "Profile Retrieved Successfully");
        }
    }
}
