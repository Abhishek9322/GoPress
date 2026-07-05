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
    public class UpdateDeliveryBoyProfileCommand:IRequest<Response<string>>
    {
        public int userId { get; set; }
        public UpdateDeliveryBoyProfileDto UpdateDeliveryBoy { get; set; }
    }
}
