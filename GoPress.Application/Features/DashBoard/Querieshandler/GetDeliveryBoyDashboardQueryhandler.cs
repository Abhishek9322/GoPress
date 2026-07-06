using GoPress.Application.Comman.Caching;
using GoPress.Application.DTOs.Dashboard;
using GoPress.Application.Features.DashBoard.Queries;
using GoPress.Application.Features.Orders.Responses;
using GoPress.Application.Interfaces.Caching;
using GoPress.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.DashBoard.Querieshandler
{
    public class GetDeliveryBoyDashboardQueryhandler : IRequestHandler<GetDeliveryBoyDashboardQuery, Response<DeliveryBoyDashBoardDto>>
    {
        private readonly ILogger<GetDeliveryBoyDashboardQueryhandler> _logger;
        private readonly IDashBoardRepository _dashBoardRepository;
        private readonly ICacheService _cacheService;
        public GetDeliveryBoyDashboardQueryhandler(ILogger<GetDeliveryBoyDashboardQueryhandler> logger,
            IDashBoardRepository dashBoardRepository,
            ICacheService cacheService)
        {
            _cacheService= cacheService;
            _logger = logger;
            _dashBoardRepository = dashBoardRepository;

        }
        public async Task<Response<DeliveryBoyDashBoardDto>> Handle(GetDeliveryBoyDashboardQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Checking dashboard cache.");

            var cachedDashboard = await _cacheService.GetAsync<DeliveryBoyDashBoardDto>(CacheKeys.DeliveryBoyDashboard);

            if (cachedDashboard != null)
            {
                _logger.LogInformation("Dashboard returned from cache.");

                return new Response<DeliveryBoyDashBoardDto>(
                    cachedDashboard,
                    "Dashboard retrieved from cache.");
            }

            _logger.LogInformation("Cache miss. Fetching dashboard from database.");


            _logger.LogInformation(
               "Admin dashboard requested.");

            var dashboardData = await _dashBoardRepository.GetDashboardForDeliveryBoyAsync(request.DeliveryBoyId);

            await _cacheService.SetAsync(
              CacheKeys.DeliveryBoyDashboard,
              dashboardData,
              TimeSpan.FromMinutes(5));

            _logger.LogInformation("Dashboard cached successfully.");

            return new Response<DeliveryBoyDashBoardDto>(
                dashboardData,
                "Dashboard retrieved successfully.");

        }
    }
}
