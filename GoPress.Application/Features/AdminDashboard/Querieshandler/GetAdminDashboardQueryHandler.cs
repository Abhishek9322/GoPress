using GoPress.Application.DTOs.Admin;
using GoPress.Application.Features.AdminDashboard.Queries;
using GoPress.Application.Features.Orders.Responses;
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
        public GetAdminDashboardQueryHandler(IAdminRepository adminRepository,ILogger<GetAdminDashboardQueryHandler> logger)
        {
            _adminRepository = adminRepository;
            _logger = logger;
        }
        public async Task<Response<AdminDashboardDto>> Handle(GetAdminDashboardQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
               "Admin dashboard requested.");

            var dashboard =await _adminRepository.GetDashboardAsync();

            _logger.LogInformation(
               "Admin dashboard retrieved successfully.");

            return new Response<AdminDashboardDto>(
                dashboard,
                "Dashboard retrieved successfully.");


        }
    }
}
