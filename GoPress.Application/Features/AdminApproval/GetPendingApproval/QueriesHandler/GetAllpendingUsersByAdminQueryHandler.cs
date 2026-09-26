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
    public class GetAllpendingUsersByAdminQueryHandler : IRequestHandler<GetAllpendingUsersByAdminQuery, Response<List<PendingUserDto>>>
    {
        private readonly IUserRepository _userRepository;
        public GetAllpendingUsersByAdminQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Response<List<PendingUserDto>>> Handle(GetAllpendingUsersByAdminQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllPendingUsers();

            var response = users.Select(user => new PendingUserDto
            {
                Id=user.Id,
                FullName=user.FullName,
                Email=user.Email,
                PhoneNumber=user.PhoneNumber,
                Role=user.Role.ToString()

            }).ToList();

            return new Response<List<PendingUserDto>>(response, "All Pending user For Approval");
        }
    }
}
