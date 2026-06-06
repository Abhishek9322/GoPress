using GoPress.Application.DTOs.Orders;
using GoPress.Application.Features.Orders.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Orders.Commands
{
    public class UpdateCustomerOrderCommand:IRequest<Response<string>>
    {
        public int OrderId { get; set; }

        public int CustomerId { get; set; }

        public UpdateCustomerOrderDto Order { get; set; }
    }
}
