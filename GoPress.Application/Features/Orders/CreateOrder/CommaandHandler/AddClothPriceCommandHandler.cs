using GoPress.Application.Features.Orders.CreateOrder.Command;
using GoPress.Application.Features.Orders.Responses;
using GoPress.Application.Interfaces.Repositories;
using GoPress.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Orders.CreateOrder.CommaandHandler
{
    public class AddClothPriceCommandHandler : IRequestHandler<AddClothPriceCommand, Response<string>>
    {
        private readonly IShopOwnerClothPriceRepository _shopOwnerClothPriceRepository;
        public AddClothPriceCommandHandler(IShopOwnerClothPriceRepository shopOwnerClothPriceRepository)
        {
            _shopOwnerClothPriceRepository = shopOwnerClothPriceRepository;
        }
        public async Task<Response<string>> Handle(AddClothPriceCommand request, CancellationToken cancellationToken)
        {
            var existing = await _shopOwnerClothPriceRepository
                 .GetByShopOwnerAndClothTypeAsync(request.ShopOwnerId,request.Price.ClothTypeId);

            if (existing != null)
            {
                return new Response<string>(
                    "Price already exists");
            }


            await _shopOwnerClothPriceRepository.AddAsync(new ShopOwnerClothPrice
            {
                ShopOwnerId = request.ShopOwnerId,
                ClothTypeId = request.Price.ClothTypeId,
                Price = request.Price.Price
            });

            return new Response<string>(
              "Price Added Successfully");
        }
    }
}
