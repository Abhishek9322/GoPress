using GoPress.Application.Features.Orders.CreateOrder.Command;
using GoPress.Application.Features.Orders.Responses;
using GoPress.Application.Interfaces.Repositories;
using MediatR;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Orders.CreateOrder.CommaandHandler
{
    public class DeleteClothPriceCommandHandle : IRequestHandler<DeleteClothPriceCommand, Response<string>>
    {
        private readonly IShopOwnerClothPriceRepository _shopOwnerClothPriceRepository;
        public DeleteClothPriceCommandHandle(IShopOwnerClothPriceRepository shopOwnerClothPriceRepository)
        {
            _shopOwnerClothPriceRepository=shopOwnerClothPriceRepository;
        }
        public async Task<Response<string>> Handle(DeleteClothPriceCommand request, CancellationToken cancellationToken)
        {
            var price = await _shopOwnerClothPriceRepository
                                  .GetByIdAsync(request.PriceId);

            if (price == null)
            {
                return new Response<string>(
                    "Price Not Found");
            }

            if (price.ShopOwnerId != request.ShopOwnerId)
            {
                return new Response<string>(
                    "Unauthorized");
            }

            await _shopOwnerClothPriceRepository.DeleteAsync(price);

            return new Response<string>("Price Deleted Successfully");

        }
    }
}
