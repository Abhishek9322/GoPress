using GoPress.Application.DTOs.Profile;
using GoPress.Application.Features.Orders.Responses;
using GoPress.Application.Features.Profile.CustomerProfile.Queries;
using GoPress.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Profile.CustomerProfile.Querieshandler
{
    public class GetCustomerProfileQueryHandler : IRequestHandler<GetCustomerProfileQuery, Response<CustomerProfileDto>>
    {
        private readonly ILogger<GetCustomerProfileQueryHandler> _logger;
        private readonly IProfileRepository _profileRepository;
        public GetCustomerProfileQueryHandler(ILogger<GetCustomerProfileQueryHandler> logger,
            IProfileRepository profileRepository)
        {
            _logger = logger;
            _profileRepository = profileRepository;
        }
        public async Task<Response<CustomerProfileDto>> Handle(GetCustomerProfileQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching Customer Info {userId}", request.userId);

            var customerprofile = await _profileRepository.GetCustomerProfileAsync(request.userId);
            if (customerprofile == null)
            {
                _logger.LogWarning(
                    "Customer {UserId} not found",
                    request.userId);

                return new Response<CustomerProfileDto>(
                    "Customer not found.");
            }

            var response = new CustomerProfileDto
            {
                UserId = customerprofile.Id,
                FullName = customerprofile.FullName,
                Email = customerprofile.Email,
                PhoneNumber = customerprofile.PhoneNumber
            };

            _logger.LogInformation(
               "Customer {UserId} profile retrieved successfully.",
               request.userId);

            return new Response<CustomerProfileDto>(
                response,
                "Customer profile retrieved successfully.");
        }
    }
}
