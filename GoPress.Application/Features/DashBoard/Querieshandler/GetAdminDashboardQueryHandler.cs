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
    public class GetAdminDashboardQueryHandler : IRequestHandler<GetAdminDashboardQuery, Response<AdminDashboardDto>>
    {
        private readonly ILogger<GetAdminDashboardQueryHandler> _logger;
        private readonly IAdminRepository _adminRepository;
        private readonly IRedisCacheService _redisCacheService;
        public GetAdminDashboardQueryHandler(IAdminRepository adminRepository,
            ILogger<GetAdminDashboardQueryHandler> logger,
            IRedisCacheService redisCacheService)
        {
            _adminRepository = adminRepository;
            _logger = logger;
            _redisCacheService = redisCacheService;
        }
        public async Task<Response<AdminDashboardDto>> Handle(GetAdminDashboardQuery request, CancellationToken cancellationToken)
        {

            var cacheKey = RedisCacheKeys.AdminDashboard;

            _logger.LogInformation( "Checking Admin Dashboard Redis cache.");

            var cachedDashboard = await _redisCacheService.GetAsync<AdminDashboardDto>(cacheKey);

            if (cachedDashboard != null)
            {
                _logger.LogInformation("Admin Dashboard Cache Hit.");

                return new Response<AdminDashboardDto>(cachedDashboard,"Dashboard retrieved successfully from cache.");
            }

            _logger.LogInformation("Admin Dashboard Cache Miss. Fetching data from database.");

            var dashboard =await _adminRepository.GetDashboardAsync();

            await _redisCacheService.SetAsync(cacheKey,dashboard,TimeSpan.FromMinutes(5));

            _logger.LogInformation("Admin Dashboard successfully stored in Redis.");

            return new Response<AdminDashboardDto>(dashboard,"Dashboard retrieved successfully.");


        }
    }
}
