using GoPress.Application.DTOs.Shops;
using GoPress.Application.Features.Orders.Responses;
using MediatR;

namespace GoPress.Application.Features.ShopOpration.GetShop.Queries
{
    public class GetShopSearchQuery: IRequest<Response<List<AvailableShopDto>>>
    {
        public int CustomerId { get; set; }
        public string Keyword { get; set; } = string.Empty;
    }
}
