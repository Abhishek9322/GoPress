using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Comman.Caching
{
    public static class CacheKeys
    {
        public const string AdminDashboard = "AdminDashboard";  //add unther with time like order ,user etc 

        public const string ShopOwnerDashboard = "ShopOwnerDashboard";

        public const string DeliveryBoyDashboard = "DeliveryBoyDashboard";

        public const string CustomerDashboard = "CustomerDashboard";

        public const string PendingOrders = "PendingOrders";

        public const string CompletedOrders = "CompletedOrders";

        public const string RejectedOrders = "RejectedOrders";

        public const string AcceptedOrders = "AcceptedOrders";

        public const string CancelledOrders = "CancelledOrders";
    }
}
