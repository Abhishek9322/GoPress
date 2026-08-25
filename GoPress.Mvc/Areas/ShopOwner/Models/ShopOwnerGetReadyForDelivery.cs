namespace GoPress.Mvc.Areas.ShopOwner.Models
{
    public class ShopOwnerGetReadyForDelivery
    {
        public int OrderId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerPhone { get; set; }

        public string PickupAddress { get; set; }

        public string DeliveryAddress { get; set; }

        public decimal TotalAmount { get; set; }

        public string Status { get; set; }
    }
}
