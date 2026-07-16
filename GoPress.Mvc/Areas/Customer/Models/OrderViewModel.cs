namespace GoPress.Mvc.Areas.Customer.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int ShopOwnerId { get; set; }

        public int? DeliveryBoyId { get; set; }

        public string PickupAddress { get; set; } = string.Empty;

        public string DeliveryAddress { get; set; } = string.Empty;

        public DateTime PickupDate { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public decimal TotalAmount { get; set; }

        public string? Notes { get; set; }

        public OrderStatusEnum  Status { get; set; }

        public List<OrderItemViewModel> OrderItems { get; set; } = new();
    }
}
