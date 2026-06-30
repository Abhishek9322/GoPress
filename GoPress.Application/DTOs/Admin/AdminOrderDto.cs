using GoPress.Application.DTOs.Orders;
using GoPress.Domain.Enums;

namespace GoPress.Application.DTOs.Admin
{
    public class AdminOrderDto
    {
        public int OrderId { get; set; }

        public string CustomerName { get; set; }

        public string ShopOwnerName { get; set; }

        public string? DeliveryBoyName { get; set; }

        public decimal TotalAmount { get; set; }

        public OrderStatusEnum Status { get; set; }

        public DateTime PickupDate { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public string PickupAddress { get; set; }

        public string DeliveryAddress { get; set; }

        public List<OrderItemResponseDto> OrderItems { get; set; }
            = new();
    }
}
