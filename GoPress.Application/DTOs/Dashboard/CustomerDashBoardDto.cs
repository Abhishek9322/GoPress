using System.Diagnostics.Contracts;
using System.Reflection.Emit;

namespace GoPress.Application.DTOs.Dashboard
{
    public class CustomerDashBoardDto
    {
        public int DeliveredOrder { get; set; }
        public int PendingOrder { get; set; }   
        public int RejectedOrder { get; set; }   
        public int CancelledOrder { get; set; }
        public int AcceptedOrder { get; set; }
        public int TotalOrder { get; set; }
        public int proceessingOrder { get; set; }
        public int OutForDeliveryOrder { get; set; }
    }
}
