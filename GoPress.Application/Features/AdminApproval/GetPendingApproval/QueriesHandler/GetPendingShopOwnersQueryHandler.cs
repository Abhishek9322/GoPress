using GoPress.Application.DTOs.Admin;
using GoPress.Application.Features.AdminApproval.GetPendingApproval.Queries;
using GoPress.Application.Features.Orders.Responses;
using GoPress.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.AdminApproval.GetPendingApproval.QueriesHandler
{
    public class GetPendingShopOwnersQueryHandler : IRequestHandler<GetPendingShopOwnersQuery, Response<List<PendingShopOwnerDto>>>
    {
        private readonly IUserRepository _userRepository;
        public GetPendingShopOwnersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Response<List<PendingShopOwnerDto>>> Handle(GetPendingShopOwnersQuery request, CancellationToken cancellationToken)
        {
            var pendingShopOwners = await _userRepository.GetPendingShopOwnersAsync();

            var response=pendingShopOwners.Select(user => new PendingShopOwnerDto
            {
                UserId = user.Id,
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
            }).ToList();

            return new Response<List<PendingShopOwnerDto>>
          (
              response,
              "Pending shop owners retrieved successfully."
          );
        }
    }
}
