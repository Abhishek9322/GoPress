using GoPress.Application.DTOs.Orders;
using GoPress.Application.Features.Orders.Responses;
using MediatR;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Orders.CreateOrder.Command
{
    public class UpdateClothPriceCommand:IRequest<Response<string>>
    {
        public int PriceId { get; set; }

        public int ShopOwnerId { get; set; }

        public UpdateClothPriceDto UpdatePrice { get; set; }
    }
}
