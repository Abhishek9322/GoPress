namespace GoPress.Mvc.Areas.Customer.Models
{
    public class CreateOrderViewModel
    {
      
        public List<AvailableShopViewModel> Shops { get; set; } = new();

        public int SelectedShopOwnerId { get; set; }

        public List<ShopPriceViewModel> PriceList { get; set; } = new();

        public string? SearchKeyword { get; set; }

        public string PickupAddress { get; set; } = string.Empty;

        public string DeliveryAddress { get; set; } = string.Empty;

        public DateTime PickupDate { get; set; }

        public string? Notes { get; set; }

        public List<OrderItemViewModel> OrderItems { get; set; } = new();
    }
}
