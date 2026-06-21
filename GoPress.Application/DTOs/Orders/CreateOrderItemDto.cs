using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.DTOs.Orders
{
    public class CreateOrderItemDto
    {
        public int ClothTypeId { get; set; }

        public int Quantity { get; set; }
    }
}
