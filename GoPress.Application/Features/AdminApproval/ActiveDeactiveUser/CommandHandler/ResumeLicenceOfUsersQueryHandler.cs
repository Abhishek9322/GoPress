using GoPress.Application.Comman.Caching;
using GoPress.Application.Features.AdminApproval.ActiveDeactiveUser.Command;
using GoPress.Application.Features.Orders.Responses;
using GoPress.Application.Interfaces.Caching;
using GoPress.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.AdminApproval.ActiveDeactiveUser.CommandHandler
{
    public class ResumeLicenceOfUsersQueryHandler : IRequestHandler<ResumeLicenceOfUsersQuery, Response<string>>
    {
        private readonly IUserRepository _userRespository;
        private readonly ILogger<ResumeLicenceOfUsersQueryHandler> _logger;
        private readonly ICacheService _cacheService;
        public ResumeLicenceOfUsersQueryHandler(IUserRepository userRepository,
                                             ILogger<ResumeLicenceOfUsersQueryHandler> logger,
                                             ICacheService cacheService)
        {
            _cacheService = cacheService;
            _userRespository = userRepository;
            _logger = logger;
            
        }
        public async Task<Response<string>> Handle(ResumeLicenceOfUsersQuery request, CancellationToken cancellationToken)
        {
            var DeActiveusers = await _userRespository.GetByIdAsync(request.userId);

            if(DeActiveusers==null)
            {
                return new Response<string>("User not Found");
            }

            if(!DeActiveusers.IsApproved)
            {
                return new Response<string>("User is not approved");
            }

            DeActiveusers.IsActive = request.IsActive;
            DeActiveusers.UpdatedAt = DateTime.UtcNow;

            await _userRespository.UpdateAsync(DeActiveusers);
            await _cacheService.RemoveAsync(CacheKeys.AdminDashboard);

            _logger.LogInformation(
                 "Admin changed user {UserId} active status to {Status}",
                request.userId,
                request.IsActive);

            return new Response<string>(
              DeActiveusers.Id.ToString(),
              request.IsActive
             ? "User Ativated successfully."
             : "User deactivated successfully.");

        }
    }
}
