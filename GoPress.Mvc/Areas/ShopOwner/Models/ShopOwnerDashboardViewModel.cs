namespace GoPress.Mvc.Areas.ShopOwner.Models
{
    public class ShopOwnerDashboardViewModel
    {
        public int TotalOrders { get; set; }

        public int PendingOrders { get; set; }

        public int AcceptedOrders { get; set; }

        public int ReadyForPickupOrders { get; set; }

        public int PickedUpOrders { get; set; }

        public int DeliveredOrders { get; set; }

        public int RejectedOrders { get; set; }

        public decimal TotalRevenue { get; set; }

        public int TodayOrders { get; set; }

        public int ThisMonthOrders { get; set; }
    }
}
