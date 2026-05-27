using GoPress.Domain.Common;

namespace GoPress.Domain.Entities
{
    public class RefreshToken:BaseEntity
    {
        public int UserId { get; set; }

        public string Token { get; set; }

        public DateTime ExpiryDate { get; set; }

        public bool IsRevoked { get; set; }

        // Navigation Property
        public ApplicationUser User { get; set; }
    }
}
