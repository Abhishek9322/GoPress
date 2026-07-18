using GoPress.Domain.Common;

namespace GoPress.Domain.Entities
{
    public class ShopOwnerProfile:BaseEntity
    {
        public int UserId { get; set; }

        public string ShopName { get; set; }

        public string ShopAddress { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Pincode { get; set; }

        public string ShopLicenseNumber { get; set; }

        public string GSTNumber { get; set; }

        public string? Description { get; set; }

        public string? ShopImageUrl { get; set; }

        public TimeOnly OpeningTime { get; set; }

        public TimeOnly ClosingTime { get; set; }

        public bool IsOpen { get; set; }

        public DateTime? LastLoginAt { get; set; }

        public DateTime? LastLogoutAt { get; set; }

        public int EstimatedDeliveryMinutes { get; set; }

        public decimal MinimumOrderAmount { get; set; }
        // Navigation Property
        public ApplicationUser User { get; set; }
    }
}
