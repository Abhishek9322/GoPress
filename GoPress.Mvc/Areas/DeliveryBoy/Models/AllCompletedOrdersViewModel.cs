namespace GoPress.Mvc.Areas.DeliveryBoy.Models
{
    public class AllCompletedOrdersViewModel
    {
        public int OrderId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerPhone { get; set; }

        public string ShopName { get; set; }

        public string PickupAddress { get; set; }

        public string DeliveryAddress { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public string Status { get; set; }
    }
}
