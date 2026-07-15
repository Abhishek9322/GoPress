namespace GoPress.Mvc.Areas.Customer.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public string CustomerId { get; set; } = string.Empty;

        public string? ShopOwnerId { get; set; }

        public string? DeliveryBoyId { get; set; }

        public string PickupAddress { get; set; } = string.Empty;

        public string DeliveryAddress { get; set; } = string.Empty;

        public DateTime PickupDate { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public decimal TotalAmount { get; set; }

        public string? Notes { get; set; }

        public string Status { get; set; } = string.Empty;
        public List<OrderItemViewModel> OrderItems { get; set; } = new();
    }
}
