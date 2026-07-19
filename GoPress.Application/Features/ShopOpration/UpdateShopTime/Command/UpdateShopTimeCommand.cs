using GoPress.Application.DTOs.Shops;
using GoPress.Application.Features.Orders.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.ShopOpration.UpdateShopTime.Command
{
    public class UpdateShopTimeCommand:IRequest<Response<string>>
    {
        public int ShopOwnerId { get; set; }
        public UpdateShopTimingDto Timing { get; set; } = null!;
    }
}
