namespace GoPress.Mvc.Areas.Customer.Models
{
    public class ShopPriceViewModel
    {
        public int Id { get; set; }

        public int ClothTypeId { get; set; }

        public string ClothName { get; set; } = string.Empty;

        public decimal Price { get; set; }
    }
}
