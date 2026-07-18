namespace GoPress.Application.DTOs.Shops
{
    public class AvailableShopDto
    {
        public int ShopOwnerId { get; set; }

        public string ShopName { get; set; } = string.Empty;

        public string ShopAddress { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public string ShopImageUrl { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal MinimumOrderAmount { get; set; }

        public int EstimatedDeliveryMinutes { get; set; }

        public bool IsOpen { get; set; }

        public TimeOnly OpeningTime { get; set; }

        public TimeOnly ClosingTime { get; set; }
    }
}
