using GoPress.Mvc.Areas.Customer.Models;

namespace GoPress.Mvc.Areas.Admin.Models
{
    public class OrdersResponseViewModel
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }

        public int ShopOwnerId { get; set; }
        public string? ShopOwnerName { get; set; }

        public int? DeliveryBoyId { get; set; }
        public string? DeliveryBoyName { get; set; }

        public string PickupAddress { get; set; }

        public string DeliveryAddress { get; set; }

        public DateTime PickupDate { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public decimal TotalAmount { get; set; }

        public string? Notes { get; set; }

        public OrderStatusEnum Status { get; set; }

        public List<OrderItemResponseViewModel> OrderItemsViewModel { get; set; }= new();

    }
}
