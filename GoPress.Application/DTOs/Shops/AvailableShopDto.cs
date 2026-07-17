namespace GoPress.Application.DTOs.Shops
{
    public class AvailableShopDto
    {
        public int ShopOwnerId { get; set; }

        public string ShopName { get; set; }

        public string ShopImageUrl { get; set; }

        public string Description { get; set; }

        public string City { get; set; }

        public string ShopAddress { get; set; }

        public int EstimatedDeliveryMinutes { get; set; }

        public decimal MinimumOrderAmount { get; set; }

        public bool IsOpen { get; set; }
    }
}
