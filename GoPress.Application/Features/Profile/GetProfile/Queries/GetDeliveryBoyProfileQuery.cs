using GoPress.Application.DTOs.Profile;
using GoPress.Application.Features.Orders.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Profile.GetProfile.Queries
{
    public class GetDeliveryBoyProfileQuery:IRequest<Response<DeliveryBoyProfileDto>>
    {
        public int userId { get; set; }  
    }
}
