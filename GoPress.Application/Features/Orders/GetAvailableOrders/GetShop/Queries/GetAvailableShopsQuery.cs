using GoPress.Application.DTOs.Shops;
using GoPress.Application.Features.Orders.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Orders.GetAvailableOrders.GetShop.Queries
{
    public class GetAvailableShopsQuery:IRequest<Response<List<AvailableShopDto>>>
    {
        public string City { get; set; }=string.Empty;
        public int CustomerId { get; set; }
    }
}
