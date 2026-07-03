using GoPress.Application.DTOs.Profile;
using GoPress.Application.Features.Orders.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Profile.CustomerProfile.Queries
{
    public class GetCustomerProfileQuery:IRequest<Response<CustomerProfileDto>>
    {
        public int userId { get; set; }
    }
}
