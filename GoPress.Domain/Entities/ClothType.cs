using GoPress.Domain.Common;

namespace GoPress.Domain.Entities
{
    public class ClothType : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<ShopOwnerClothPrice>ShopOwnerClothPrices{ get; set; }
    }
}
