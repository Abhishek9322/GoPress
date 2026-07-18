using GoPress.Application.DTOs.Shops;
using GoPress.Application.Features.DashBoard.Queries;
using GoPress.Application.Features.Orders.GetAvailableOrders.GetShop.Queries;
using GoPress.Application.Features.Orders.Responses;
using GoPress.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace GoPress.Application.Features.Orders.GetAvailableOrders.GetShop.QueriesHandler
{
    public class GetAvailableShopsQueryHandler : IRequestHandler<GetAvailableShopsQuery, Response<List<AvailableShopDto>>>
    {
        private readonly ILogger<GetAvailableShopsQueryHandler> _logger;
        private readonly IUserRepository _userRepository;
        public GetAvailableShopsQueryHandler(ILogger<GetAvailableShopsQueryHandler> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }
        public async Task<Response<List<AvailableShopDto>>> Handle(GetAvailableShopsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
               "Customer {CustomerId} requested available shops.",
               request.CustomerId);

            var customer = await _userRepository
                .GetCustomerWithProfileAsync(request.CustomerId);

            if (customer == null)
            {
                _logger.LogWarning(
                    "Customer {CustomerId} was not found.",
                    request.CustomerId);

                return new Response<List<AvailableShopDto>>(
                    "Customer not found.");
            }

            if (customer.CustomerProfile == null)
            {
                _logger.LogWarning(
                    "Customer profile not found for Customer {CustomerId}.",
                    request.CustomerId);

                return new Response<List<AvailableShopDto>>(
                    "Customer profile not found.");
            }

            var city = customer.CustomerProfile.City;

            _logger.LogInformation(
                "Searching available shops in city {City}.",
                city);

            var shops = await _userRepository
                .GetAvailableShopsAsync(city);  //problem in the oping time and the closing time fixing here have to make api for this after thst it will fix 

            if (!shops.Any())
            {
                _logger.LogWarning(
                    "No shops available in city {City}.",
                    city);

                return new Response<List<AvailableShopDto>>(
                    new List<AvailableShopDto>(),
                    "No available shops found.");
            }

            _logger.LogInformation(
                "{Count} shops found in city {City}.",
                shops.Count,
                city);

            var response = shops.Select(shop => new AvailableShopDto
            {
                ShopOwnerId = shop.Id,
                ShopName = shop.ShopOwnerProfile.ShopName,
                ShopAddress = shop.ShopOwnerProfile.ShopAddress,
                City = shop.ShopOwnerProfile.City,
                State = shop.ShopOwnerProfile.State,
                ShopImageUrl = shop.ShopOwnerProfile.ShopImageUrl,
                Description = shop.ShopOwnerProfile.Description,
                MinimumOrderAmount = shop.ShopOwnerProfile.MinimumOrderAmount,
                EstimatedDeliveryMinutes = shop.ShopOwnerProfile.EstimatedDeliveryMinutes,
                IsOpen = shop.ShopOwnerProfile.IsOpen,
                OpeningTime = shop.ShopOwnerProfile.OpeningTime,
                ClosingTime = shop.ShopOwnerProfile.ClosingTime

            }).ToList();

            _logger.LogInformation(
                "Successfully returned {Count} available shops.",
                response.Count);

            return new Response<List<AvailableShopDto>>(
                response,
                "Available Shops Retrieved Successfully");
        }
    }
}
