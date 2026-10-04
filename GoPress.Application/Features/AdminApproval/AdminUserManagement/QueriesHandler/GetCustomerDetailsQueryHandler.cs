using GoPress.Application.DTOs.Admin.UserManegment;
using GoPress.Application.Features.AdminApproval.AdminUserManagement.Queries;
using GoPress.Application.Features.Orders.Responses;
using GoPress.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.AdminApproval.AdminUserManagement.QueriesHandler
{
    public class GetCustomerDetailsQueryHandler : IRequestHandler<GetCustomerDetailsQuery, Response<AllCutomerProfileDto>>
    {
        private readonly IAdminUserManegementRepository _adminUserManegementRepository;
        public GetCustomerDetailsQueryHandler(IAdminUserManegementRepository adminUserManegementRepository)
        {
            _adminUserManegementRepository = adminUserManegementRepository;

        }

        public async Task<Response<AllCutomerProfileDto>> Handle(GetCustomerDetailsQuery request, CancellationToken cancellationToken)
        {
            var customer = await _adminUserManegementRepository.GetCustomerDetails(request.CustomerId);
              
            if (customer == null)
            {
                return new Response<AllCutomerProfileDto>(null,"Customer not found.");
            }

            var response = new AllCutomerProfileDto
            {
                UserId = customer.Id,
                Address = customer.CustomerProfile?.Address,
                City = customer.CustomerProfile?.City,
                State = customer.CustomerProfile?.State,
                Pincode = customer.CustomerProfile?.Pincode,
                AllApplicationUserProfileDto =new AllApplicationUserProfileDto
                    {
                        FullName = customer.FullName,
                        Email = customer.Email,
                        PhoneNumber = customer.PhoneNumber,
                        Role = customer.Role.ToString()
                    }
            };

            return new Response<AllCutomerProfileDto>(response,"Get Customer Details Successfully.");
        }
    }

    
}
