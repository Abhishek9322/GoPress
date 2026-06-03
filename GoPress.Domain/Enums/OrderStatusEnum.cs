using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Domain.Enums
{
    public enum OrderStatusEnum
    {
        Pending = 1,
        Accepted = 2,
        PickupAssigned = 3,
        PickedUp = 4,
        Processing = 5,
        OutForDelivery = 6,
        Delivered = 7,
        Cancelled = 8,
        Rejected = 9
    }
}
