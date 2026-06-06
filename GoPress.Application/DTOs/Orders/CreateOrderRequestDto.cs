namespace GoPress.Application.DTOs.Orders
{
    public class CreateOrderRequestDto
    {
        public int ShopOwnerId { get; set; }

        public string PickupAddress { get; set; }

        public string DeliveryAddress { get; set; }

        public DateTime PickupDate { get; set; }

        public string? Notes { get; set; }

        public List<CreateOrderItemDto> OrderItems { get; set; }
    }
}
