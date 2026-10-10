using GoPress.Mvc.Areas.Customer.Models;

namespace GoPress.Mvc.Areas.Admin.Models
{
    public class AdminOrderViewModel 
    {
        public int OrderId { get; set; }

        public string CustomerName { get; set; }

        public string ShopOwnerName { get; set; }

        public string? DeliveryBoyName { get; set; }

        public decimal TotalAmount { get; set; }

        public OrderStatusEnum Status { get; set; }

        public DateTime PickupDate { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public string PickupAddress { get; set; }

        public string DeliveryAddress { get; set; }

        public List<OrderItemResponseViewModel> OrderItems { get; set; } = new();
    }

}
