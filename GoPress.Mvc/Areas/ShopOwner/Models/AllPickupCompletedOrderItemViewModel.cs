namespace GoPress.Mvc.Areas.ShopOwner.Models
{
    public class AllPickupCompletedOrderItemViewModel
    {
        public int Id { get; set; }

        public int ClothTypeId { get; set; }
        public string ClothName { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
