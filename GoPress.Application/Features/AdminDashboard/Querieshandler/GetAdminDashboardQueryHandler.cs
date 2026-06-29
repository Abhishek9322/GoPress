using GoPress.Application.Comman.Caching;
using GoPress.Application.DTOs.Admin;
using GoPress.Application.Features.AdminDashboard.Queries;
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

namespace GoPress.Application.Features.AdminDashboard.Querieshandler
{
    public class GetAdminDashboardQueryHandler : IRequestHandler<GetAdminDashboardQuery, Response<AdminDashboardDto>>
    {
        private readonly ILogger<GetAdminDashboardQueryHandler> _logger;
        private readonly IAdminRepository _adminRepository;   
        private readonly ICacheService _cacheService;
        public GetAdminDashboardQueryHandler(IAdminRepository adminRepository,
            ILogger<GetAdminDashboardQueryHandler> logger,
            ICacheService cacheService)
        {
            _adminRepository = adminRepository;
            _logger = logger;
            _cacheService = cacheService;
        }
        public async Task<Response<AdminDashboardDto>> Handle(GetAdminDashboardQuery request, CancellationToken cancellationToken)
        {

            _logger.LogInformation("Checking dashboard cache.");

            var cachedDashboard=await _cacheService.GetAsync<AdminDashboardDto>(CacheKeys.AdminDashboard);

            if (cachedDashboard != null)
            {
                _logger.LogInformation("Dashboard returned from cache.");

                return new Response<AdminDashboardDto>(
                    cachedDashboard,
                    "Dashboard retrieved from cache.");
            }

            _logger.LogInformation("Cache miss. Fetching dashboard from database.");


            _logger.LogInformation(
               "Admin dashboard requested.");

            var dashboard =await _adminRepository.GetDashboardAsync();

            await _cacheService.SetAsync(
                CacheKeys.AdminDashboard,
                dashboard,
                TimeSpan.FromMinutes(5));

            _logger.LogInformation("Dashboard cached successfully.");

            return new Response<AdminDashboardDto>(
                dashboard,
                "Dashboard retrieved successfully.");


        }
    }
}
