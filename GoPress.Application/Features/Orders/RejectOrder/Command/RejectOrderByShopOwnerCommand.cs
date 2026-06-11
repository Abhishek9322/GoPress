using GoPress.Application.Features.Orders.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Orders.RejectOrder.command
{
    public class RejectOrderByShopOwnerCommand:IRequest<Response<string>>
    {
        public int OrderId { get; set; }
        public int ShopOwnerId { get; set; }
    }
}
