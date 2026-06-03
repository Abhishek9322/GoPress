using GoPress.Domain.Common;

namespace GoPress.Domain.Entities
{
    public class OrderItem:BaseEntity
    {
        public int OrderId { get; set; }

        // Example:
        // Shirt
        // Pant
        // Saree

        public string ClothName { get; set; }

        // Quantity
        public int Quantity { get; set; }

        // Price Per Cloth
        public decimal Price { get; set; }

        // Total
        public decimal TotalPrice { get; set; }

        // NAVIGATION

        public Order Order { get; set; }

    }
}
