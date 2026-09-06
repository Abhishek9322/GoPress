namespace GoPress.Mvc.Areas.DeliveryBoy.Models
{
    public class AllReadyFordeliveryOrdersViewModel
    {
        public int OrderId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerPhone { get; set; }

        public string DeliveryAddress { get; set; }

        public decimal TotalAmount { get; set; }
        public string Status { get; set; }

    }
}
