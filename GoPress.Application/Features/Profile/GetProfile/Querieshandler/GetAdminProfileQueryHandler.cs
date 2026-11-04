using GoPress.Application.DTOs.Admin.AdminProfile;
using GoPress.Application.Features.Orders.Responses;
using GoPress.Application.Features.Profile.GetProfile.Queries;
using GoPress.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Profile.GetProfile.Querieshandler
{
    public class GetAdminProfileQueryHandler:IRequestHandler<GetAdminProfileQuery, Response<AdminProfileResponseDto>>
    {
        private readonly IProfileRepository _profileRepository;
        public GetAdminProfileQueryHandler(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }
        public async Task<Response<AdminProfileResponseDto>> Handle(GetAdminProfileQuery request, CancellationToken cancellationToken)
        {
            var adminProfile = await _profileRepository.GetAdminProfile(request.AdminId,cancellationToken);

            if(adminProfile == null)
            {
                throw new Exception("Admin Can Not Found .");
            }

            var admin= new AdminProfileResponseDto
            {
                Id=adminProfile.Id,
                FullName=adminProfile.FullName,
                Email = adminProfile.Email,
                PhoneNumber = adminProfile.PhoneNumber,
                Role = adminProfile.Role.ToString()
               
            };

            return new Response<AdminProfileResponseDto>(admin,"Get Admin Sccuessfully ");
        }
    }
}
