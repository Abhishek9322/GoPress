using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.DTOs.Orders
{
    public class CreateOrderItemDto
    {
        public string ClothName { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
