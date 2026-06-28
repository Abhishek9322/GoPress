using GoPress.Application.DTOs.Admin;
using GoPress.Application.Features.AdminApproval.GetApprovedUsers.Queries;
using GoPress.Application.Features.Orders.Responses;
using GoPress.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.AdminApproval.GetApprovedUsers.QueriesHandler
{
    public class GetApprovedShopOwnersQueryHandler : IRequestHandler<GetApprovedShopOwnersQuery, Response<List<PendingShopOwnerDto>>>
    {
        private readonly IUserRepository _userRepository;
   
        public GetApprovedShopOwnersQueryHandler(IUserRepository userRepository)
        {
            
            _userRepository = userRepository;
        }
        public async Task<Response<List<PendingShopOwnerDto>>> Handle(GetApprovedShopOwnersQuery request, CancellationToken cancellationToken)
        {
           
            var approvedShopOwners =await _userRepository.GetApprovedShopownerAsync();

            var response =
               approvedShopOwners.Select(user => new PendingShopOwnerDto
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
                   ShopLicenseNumber =user.ShopOwnerProfile.ShopLicenseNumber,
                   GSTNumber =user.ShopOwnerProfile.GSTNumber
               }).ToList();

            return new Response<List<PendingShopOwnerDto>>(
                response,
                "Approved shop owners retrieved successfully.");



        }

    }
}
