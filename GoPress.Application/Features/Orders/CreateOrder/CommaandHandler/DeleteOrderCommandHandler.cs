using GoPress.Application.Comman.Caching;
using GoPress.Application.Features.Orders.CreateOrder.Command;
using GoPress.Application.Features.Orders.Responses;
using GoPress.Application.Interfaces.Caching;
using GoPress.Application.Interfaces.Repositories;
using GoPress.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Orders.CreateOrder.CommaandHandlers
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Response<string>>
    {
        private readonly IOrderRepository _repository;
        private readonly ILogger<DeleteOrderCommandHandler> _logger;
        private readonly ICacheService _cacheService;
        public DeleteOrderCommandHandler(IOrderRepository repository,
            ILogger<DeleteOrderCommandHandler> logger,
            ICacheService cacheService)
        {
            _repository = repository;
            _logger = logger;
            _cacheService = cacheService;
        }
        public async Task<Response<string>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _repository.GetByIdAsync(request.OrderId);
            if (order == null)
            {
                return new Response<string>(
                    null,
                    "Order Not Found");
            }

            // SECURITY CHECK

            if (order.CustomerId != request.CustomerId)
            {
                return new Response<string>(
                    null,
                    "You Cannot Delete This Order");
            }

            // STATUS CHECK

            if (order.Status != OrderStatusEnum.Pending)
            {
                return new Response<string>(
                    null,
                    "Only Pending Orders Can Be Deleted");
            }

            _logger.LogInformation(
                                  "Customer {CustomerId} requested deletion of order {OrderId}",
                       request.CustomerId,
                        request.OrderId);

            await _repository.DeleteAsync(order);

            await _cacheService.RemoveAsync(CacheKeys.AdminDashboard);  
            _logger.LogInformation(
                "Order {OrderId} deleted successfully",
               request.OrderId);

            return new Response<string>(
                "Success",
                "Order Deleted Successfully");
        }


    }
}
