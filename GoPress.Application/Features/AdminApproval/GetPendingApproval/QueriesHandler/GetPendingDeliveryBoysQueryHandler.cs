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
    public class GetPendingDeliveryBoysQueryHandler : IRequestHandler<GetPendingDeliveryBoysQuery, Response<List<PendingDeliveryBoyDto>>>
    {
        private readonly IUserRepository _userRepository;
        public GetPendingDeliveryBoysQueryHandler(IUserRepository userRepository)
        {

            _userRepository = userRepository;
        }
        public async Task<Response<List<PendingDeliveryBoyDto>>> Handle(GetPendingDeliveryBoysQuery request, CancellationToken cancellationToken)
        {
            var pendingDeliveryBoy = await _userRepository.GetPendingDeliveryBoysAsync();

            var response = pendingDeliveryBoy.Select(users => new PendingDeliveryBoyDto
            {
                UserId = users.Id,
                FullName = users.FullName,
                Email = users.Email,
                PhoneNumber = users.PhoneNumber,
                Address = users.DeliveryBoyProfile.Address,
                City = users.DeliveryBoyProfile.City,
                State = users.DeliveryBoyProfile.State,
                Pincode = users.DeliveryBoyProfile.Pincode,
                BikeNumber = users.DeliveryBoyProfile.BikeNumber,
                LicenseNumber = users.DeliveryBoyProfile.LicenseNumber,
                AadhaarNumber = users.DeliveryBoyProfile.AadhaarNumber
            }).ToList();

            return new Response<List<PendingDeliveryBoyDto>>(response,
                "Pending Delivery Boys Retrieved Successfully");

        }
    }
}
