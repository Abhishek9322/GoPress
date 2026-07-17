using GoPress.Domain.Common;

namespace GoPress.Domain.Entities
{
    public class ShopReview : BaseEntity
    {
        public int CustomerId { get; set; }

        public int ShopOwnerId { get; set; }

        public int OrderId { get; set; }

        public int Rating { get; set; }      //1-5

        public string? Review { get; set; }

        public ApplicationUser Customer { get; set; }

        public ApplicationUser ShopOwner { get; set; }

        public Order Order { get; set; }
    }
}
