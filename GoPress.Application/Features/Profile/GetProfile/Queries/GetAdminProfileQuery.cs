using GoPress.Application.DTOs.Admin.AdminProfile;
using GoPress.Application.Features.Orders.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Profile.GetProfile.Queries
{
    public class GetAdminProfileQuery:IRequest<Response<AdminProfileResponseDto>>
    {
        public int AdminId { get; set; }
     
    }
}
