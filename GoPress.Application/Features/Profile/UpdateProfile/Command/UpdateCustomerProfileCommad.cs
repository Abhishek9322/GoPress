using GoPress.Application.DTOs.Orders;
using GoPress.Application.DTOs.Profile;
using GoPress.Application.Features.Orders.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Profile.UpdateProfile.Command
{
    public class UpdateCustomerProfileCommad:IRequest<Response<UpdateCustomerProfileDto>>
    {
        public int UserId { get; set; }
        public UpdateCustomerProfileDto Profile { get; set; }
    }
}
