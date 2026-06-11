using GoPress.Application.DTOs.Orders;
using GoPress.Application.Features.Orders.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Orders.CreateOrder.Command
{
    public class CreateOrderCommand:IRequest<Response<int>>
    {
        public int CustomerId { get; set; }

        public CreateOrderRequestDto Order { get; set; }
    }
}
