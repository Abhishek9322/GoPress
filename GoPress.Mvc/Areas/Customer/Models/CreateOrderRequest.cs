namespace GoPress.Mvc.Areas.Customer.Models
{
    public class CreateOrderRequest
    {
        public int ShopOwnerId { get; set; }

        public string PickupAddress { get; set; } = string.Empty;

        public string DeliveryAddress { get; set; } = string.Empty;

        public DateTime PickupDate { get; set; }

        public string? Notes { get; set; }

        public List<CreateOrderItemRequest> OrderItems { get; set; } = new();
    }
     
}
