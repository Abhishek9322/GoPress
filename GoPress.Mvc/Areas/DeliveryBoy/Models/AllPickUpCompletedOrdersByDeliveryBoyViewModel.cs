using GoPress.Mvc.Areas.Customer.Models;
using GoPress.Mvc.Areas.ShopOwner.Models;

namespace GoPress.Mvc.Areas.DeliveryBoy.Models
{
    public class AllPickUpCompletedOrdersByDeliveryBoyViewModel
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int ShopOwnerId { get; set; }

        public int? DeliveryBoyId { get; set; }

        public string PickupAddress { get; set; }

        public string DeliveryAddress { get; set; }

        public DateTime PickupDate { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public decimal TotalAmount { get; set; }

        public string? Notes { get; set; }

        public OrderStatusEnum Status { get; set; }

        public List<AllPickupCompletedOrderItemDeliveryBoyViewModel> OrderItems { get; set; }
    }
}
