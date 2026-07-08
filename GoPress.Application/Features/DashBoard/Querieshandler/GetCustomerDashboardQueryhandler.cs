using GoPress.Application.Comman.Caching;
using GoPress.Application.DTOs.Dashboard;
using GoPress.Application.Features.DashBoard.Queries;
using GoPress.Application.Features.Orders.Responses;
using GoPress.Application.Interfaces.Caching;
using GoPress.Application.Interfaces.Repositories;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.DashBoard.Querieshandler
{
    public class GetCustomerDashboardQueryhandler : IRequestHandler<GetCustomerDashboardQuery, Response<CustomerDashBoardDto>>
    {
        private readonly ILogger<GetCustomerDashboardQueryhandler> _logger;
        private readonly ICacheService _cacheService;
        private readonly IDashBoardRepository _dashBoardRepository;
        public GetCustomerDashboardQueryhandler(ILogger<GetCustomerDashboardQueryhandler> logger,
            ICacheService cacheService,
            IDashBoardRepository dashBoardRepository)
        {
            _cacheService = cacheService;   
            _logger = logger;
            _dashBoardRepository = dashBoardRepository;
        }
        public async Task<Response<CustomerDashBoardDto>> Handle(GetCustomerDashboardQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Checking Dashboard cache ..");

            var chachedDashBoard = await _cacheService.GetAsync<CustomerDashBoardDto>(CacheKeys.CancelledOrders);

            if (chachedDashBoard != null)
            {
                _logger.LogInformation("Dashboard returned from cache.");

                return new Response<CustomerDashBoardDto>(
                   chachedDashBoard,
                   "Dashboard retrieved from cache.");
            }

            _logger.LogInformation("Cache miss. Fetching dashboard from database.");

            _logger.LogInformation(
              "Customer dashboard requested.");

            var dashboardData = await _dashBoardRepository.GetDashboardForCustomerAsync(request.customerUserId);

            await _cacheService.SetAsync(
                CacheKeys.CustomerDashboard,
                dashboardData,
                TimeSpan.FromMinutes(5));

            _logger.LogInformation("Dashboard cached successfully.");

            return new Response<CustomerDashBoardDto>(
                dashboardData,
                "Dashboard retrieved successfully.");
        }
        
    }
}
