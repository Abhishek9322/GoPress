using GoPress.Application.Features.Orders.Commands;
using GoPress.Application.Features.Orders.Responses;
using GoPress.Application.Interfaces.Repositories;
using GoPress.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Orders.CommandHandler
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Response<string>>
    {
        private readonly IOrderRepository _repository;
        public DeleteOrderCommandHandler(IOrderRepository repository)
        {
            _repository = repository;
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
            await _repository.DeleteAsync(order);

            return new Response<string>(
                "Success",
                "Order Deleted Successfully");
        }


    }
}
