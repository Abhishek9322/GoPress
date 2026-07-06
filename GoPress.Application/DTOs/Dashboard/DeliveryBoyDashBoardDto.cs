namespace GoPress.Application.DTOs.Dashboard
{
    public class DeliveryBoyDashBoardDto
    {
        public int AvailableOrders { get;set; }
        public int AcceptedOrders { get; set; }
        public int PendingPickupOrders { get; set; }
        public int PickedUpOrders { get; set; }
        public int OutForDeliveryOrders { get; set; }
        public int ReadyForDeliveryOrders { get; set; }
        public int DeliveredOrders { get; set; }
        public int TodayDeliveries { get; set; }
        public int ThisMonthDeliveries { get; set; }

    }
}


