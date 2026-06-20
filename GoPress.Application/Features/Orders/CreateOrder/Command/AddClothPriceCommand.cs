using GoPress.Application.DTOs.Orders;
using GoPress.Application.Features.Orders.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Orders.CreateOrder.Command
{
    public class AddClothPriceCommand:IRequest<Response<string>>
    {
        public int ShopOwnerId { get; set; }
        public AddClothPriceDto Price { get; set; }
    }
}
