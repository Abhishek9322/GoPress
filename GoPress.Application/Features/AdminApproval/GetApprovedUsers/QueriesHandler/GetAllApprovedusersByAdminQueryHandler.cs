using GoPress.Application.DTOs.Admin;
using GoPress.Application.Features.AdminApproval.GetApprovedUsers.Queries;
using GoPress.Application.Features.Orders.Responses;
using GoPress.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.AdminApproval.GetApprovedUsers.QueriesHandler
{
    public class GetAllApprovedusersByAdminQueryHandler : IRequestHandler<GetAllApprovedusersByAdminQuery, Response<List<ApprovedUsersDto>>>
    {
        private readonly IUserRepository _userRepository;
        public GetAllApprovedusersByAdminQueryHandler(IUserRepository userRepository)
        {
            _userRepository= userRepository;
        }
        public async Task<Response<List<ApprovedUsersDto>>> Handle(GetAllApprovedusersByAdminQuery request, CancellationToken cancellationToken)
        {
            var approvedusers = await _userRepository.GetAllApprovedUsers();

            var users = approvedusers.Select(user => new ApprovedUsersDto
            {
                UserId=user.Id,
                FullName=user.FullName,
                Email=user.Email,
                PhoneNumber=user.PhoneNumber             

            }).ToList();

            return new Response<List<ApprovedUsersDto>>(users,"All Approved User By Admin .");
        }
    }
}
