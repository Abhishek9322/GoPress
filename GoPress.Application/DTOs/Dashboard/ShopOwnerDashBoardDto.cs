using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.DTOs.Dashboard
{
    public class ShopOwnerDashBoardDto
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
