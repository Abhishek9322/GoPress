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

        // Navigation Property
        public ApplicationUser User { get; set; }
    }
}
