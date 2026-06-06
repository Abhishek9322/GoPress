namespace GoPress.Application.DTOs.Orders
{
    public class OrderItemResponseDto
    {
        public int Id { get; set; }

        public string ClothName { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
