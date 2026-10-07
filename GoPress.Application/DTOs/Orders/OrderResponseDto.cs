using GoPress.Domain.Enums;

namespace GoPress.Application.DTOs.Orders
{
    public class OrderResponseDto
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public string? CustomerName { get; set; }

        public int ShopOwnerId { get; set; }

        public string? ShopOwnerName { get; set; }
        public int? DeliveryBoyId { get; set; }

        public string? DeliveryBoyName { get; set; }
        public string PickupAddress { get; set; } 

        public string DeliveryAddress { get; set; }

        public DateTime PickupDate { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public decimal TotalAmount { get; set; }

        public string? Notes { get; set; }

        public OrderStatusEnum Status { get; set; }

        public List<OrderItemResponseDto> OrderItems { get; set; } = new();
    }
}
