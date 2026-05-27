using GoPress.Domain.Common;

namespace GoPress.Domain.Entities
{
    public class DeliveryBoyProfile:BaseEntity
    {
        public int UserId { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Pincode { get; set; }

        public string BikeNumber { get; set; }

        public string LicenseNumber { get; set; }

        public string AadhaarNumber { get; set; }

        // Navigation Property
        public ApplicationUser User { get; set; }

    }
}
