using GoPress.Application.DTOs.Orders;
using GoPress.Application.Features.Orders.Responses;
using GoPress.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Orders.GetAvailableOrders.Queries
{
    public class GetMyPriceListQuery:IRequest<Response<List<ShopOwnerClothPriceDto>>>
    {
        public int ShopOwnerId { get; set; }    
    }
}
