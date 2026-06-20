namespace GoPress.Application.DTOs.Orders
{
    public class ShopOwnerClothPriceDto
    {
        public int Id { get; set; }

        public int ClothTypeId { get; set; }

        public string ClothName { get; set; }

        public decimal Price { get; set; }
    }
}
