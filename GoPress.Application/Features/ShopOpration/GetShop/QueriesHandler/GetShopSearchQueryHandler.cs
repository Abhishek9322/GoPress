using GoPress.Application.DTOs.Shops;
using GoPress.Application.Features.Orders.Responses;
using GoPress.Application.Features.ShopOpration.GetShop.Queries;
using GoPress.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GoPress.Application.Features.ShopOpration.GetShop.QueriesHandler
{
    public partial class GetAvailableShopsQueryHandler
    {
        public class GetShopSearchQueryHandler : IRequestHandler<GetShopSearchQuery, Response<List<AvailableShopDto>>>
        {
            private readonly ILogger<GetShopSearchQueryHandler> _logger;
            private readonly IUserRepository _userRepository;
            private readonly ISearchRepository _searchRepository;
            public GetShopSearchQueryHandler(IUserRepository userRepository,
                ILogger<GetShopSearchQueryHandler> logger,
                ISearchRepository searchRepository)
            {
                _logger = logger;
                _userRepository = userRepository;
                _searchRepository = searchRepository;
            }
            public async Task<Response<List<AvailableShopDto>>> Handle(GetShopSearchQuery request, CancellationToken cancellationToken)
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

                var shops = await _searchRepository.SearchShopsAsync(request.Keyword);


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
}
