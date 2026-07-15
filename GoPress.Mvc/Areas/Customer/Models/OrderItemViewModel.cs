namespace GoPress.Mvc.Areas.Customer.Models
{
    public class OrderItemViewModel
    {
        public int Id { get; set; }

        public string ClothName { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
