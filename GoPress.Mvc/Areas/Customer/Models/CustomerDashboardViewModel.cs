namespace GoPress.Mvc.Areas.Customer.Models
{
    public class CustomerDashboardViewModel
    {
        public int DeliveredOrder { get; set; }

        public int PendingOrder { get; set; }

        public int RejectedOrder { get; set; }

        public int CancelledOrder { get; set; }

        public int AcceptedOrder { get; set; }

        public int TotalOrder { get; set; }

        public int ProceessingOrder { get; set; }

        public int OutForDeliveryOrder { get; set; }
    }
}
