namespace GoPress.Application.DTOs.Dashboard
{
    public class AdminDashboardDto
    {
        public int TotalCustomers { get; set; }

        public int TotalShopOwners { get; set; }

        public int TotalDeliveryBoys { get; set; }

        public int PendingShopOwnerApprovals { get; set; }

        public int PendingDeliveryBoyApprovals { get; set; }

        public int ActiveUsers { get; set; }

        public int InactiveUsers { get; set; }

        public int TotalOrders { get; set; }

        public int PendingOrders { get; set; }

        public int CompletedOrders { get; set; }
    }
}
