using GoPress.Domain.Common;

namespace GoPress.Domain.Entities
{
    public class ShopOwnerClothPrice : BaseEntity
    {
        public int ShopOwnerId { get; set; }

        public int ClothTypeId { get; set; }

        public decimal Price { get; set; }

        // Navigation

        public ApplicationUser ShopOwner { get; set; }

        public ClothType ClothType { get; set; }
    }
}
