using GoPress.Domain.Common;
using GoPress.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Domain.Entities
{
    public class Order:BaseEntity
    {
        // CUSTOMER
        public int CustomerId { get; set; }

        // SHOP OWNER
        public int ShopOwnerId { get; set; }

        // DELIVERY BOY
        public int? DeliveryBoyId { get; set; }

        // ORDER DETAILS
        public string PickupAddress { get; set; }

        public string DeliveryAddress { get; set; }

        public DateTime PickupDate { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public decimal TotalAmount { get; set; }

        public string? Notes { get; set; }

        // ORDER STATUS
        public OrderStatusEnum Status { get; set; }
            = OrderStatusEnum.Pending;

        // NAVIGATION PROPERTIES

        public ApplicationUser Customer { get; set; }

        public ApplicationUser ShopOwner { get; set; }

        public ApplicationUser? DeliveryBoy { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
