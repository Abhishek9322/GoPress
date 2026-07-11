namespace GoPress.Mvc.Areas.Customer.Models
{
    public class CustomerDashboardViewModel
    {
        public int TotalOrders { get; set; }

        public int PendingOrders { get; set; }

        public int AcceptedOrders { get; set; }

        public int PickedUpOrders { get; set; }

        public int DeliveredOrders { get; set; }

        public decimal TotalSpent { get; set; }
    }
}
