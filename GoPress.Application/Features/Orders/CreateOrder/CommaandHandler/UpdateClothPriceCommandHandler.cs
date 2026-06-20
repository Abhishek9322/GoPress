using GoPress.Application.Features.Orders.CreateOrder.Command;
using GoPress.Application.Features.Orders.Responses;
using GoPress.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Orders.CreateOrder.CommaandHandler
{
    public class UpdateClothPriceCommandHandler : IRequestHandler<UpdateClothPriceCommand, Response<string>>
    {
        private readonly IShopOwnerClothPriceRepository _shopOwnerClothPriceRepository;
        public UpdateClothPriceCommandHandler(IShopOwnerClothPriceRepository shopOwnerClothPriceRepository  )
        {
            _shopOwnerClothPriceRepository = shopOwnerClothPriceRepository;
        }
        public async Task<Response<string>> Handle(UpdateClothPriceCommand request, CancellationToken cancellationToken)
        {
            var price =await _shopOwnerClothPriceRepository
                 .GetByIdAsync(request.PriceId);

            if (price == null)
            {
                return new Response<string>(
                    "Price Not Found");
            }

            if(price.ShopOwnerId != request.ShopOwnerId)
            {
                return new Response<string>(
                    "You are not authorized to update this price");
            }

            price.Price = request.UpdatePrice.Price;

            await _shopOwnerClothPriceRepository.UpdateAsync(price);

            return new Response<string>(
              "Price Updated Successfully");
        }
    }
}
