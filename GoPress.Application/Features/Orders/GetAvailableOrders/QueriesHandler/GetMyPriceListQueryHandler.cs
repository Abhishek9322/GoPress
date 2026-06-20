using GoPress.Application.DTOs.Orders;
using GoPress.Application.Features.Orders.GetAvailableOrders.Queries;
using GoPress.Application.Features.Orders.Responses;
using GoPress.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Orders.GetAvailableOrders.QueriesHandler
{
    public class GetMyPriceListQueryHandler : IRequestHandler<GetMyPriceListQuery, Response<List<ShopOwnerClothPriceDto>>>
    {
        private readonly IShopOwnerClothPriceRepository _shopOwnerClothPriceRepository;
        public GetMyPriceListQueryHandler(IShopOwnerClothPriceRepository shopOwnerClothPriceRepository)
        {
            _shopOwnerClothPriceRepository=shopOwnerClothPriceRepository;
        }
        public async Task<Response<List<ShopOwnerClothPriceDto>>> Handle(GetMyPriceListQuery request, CancellationToken cancellationToken)
        {
            var prices = await _shopOwnerClothPriceRepository
                               .GetByShopOwnerIdAsync(request.ShopOwnerId);

            var result = prices.Select(priceses=>new ShopOwnerClothPriceDto
            {
                Id = priceses.Id,
                ClothTypeId = priceses.ClothTypeId,
                ClothName=priceses.ClothType.Name,
                Price = priceses.Price

            }).ToList();

            return new Response<List<ShopOwnerClothPriceDto>>(
               result,
               "Price List");


        }
    }
}
