using GoPress.Application.DTOs.Admin;
using GoPress.Application.Features.AdminApproval.GetPendingApproval.DeActiveUsers.Queries;
using GoPress.Application.Features.Orders.Responses;
using GoPress.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.AdminApproval.GetPendingApproval.DeActiveUsers.QueriesHandler
{
    public class GetAllDeActiveUsersQueryHandler : IRequestHandler<GetAllDeActiveUsersQuery, Response<List<DeActiveUsersDto>>>
    {
        private readonly IUserRepository _userRepository;
        public GetAllDeActiveUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository= userRepository;
        }
        public async Task<Response<List<DeActiveUsersDto>>> Handle(GetAllDeActiveUsersQuery request, CancellationToken cancellationToken)
        {
            var DeActiveusers = await _userRepository.GetAllDeActiveUsers();

            var response = DeActiveusers.Select(x => new DeActiveUsersDto
            {
                UserId = x.Id,
                FullName = x.FullName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                IsActive=x.IsActive,
                Role = x.Role.ToString()
            }).ToList();

            return new Response<List<DeActiveUsersDto>>(response, "DeActive Users Fetched Successfully");
        }
    }
}
