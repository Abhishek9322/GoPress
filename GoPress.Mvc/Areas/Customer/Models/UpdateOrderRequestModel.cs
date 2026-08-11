namespace GoPress.Mvc.Areas.Customer.Models
{
    public class UpdateOrderRequestModel
    {
        public string PickupAddress { get; set; } = string.Empty;

        public string DeliveryAddress { get; set; } = string.Empty;

        public DateTime PickupDate { get; set; }

        public string? Notes { get; set; }

        public List<UpdateOrderItemRequestModel> OrderItems { get; set; } = new();
    }
}
