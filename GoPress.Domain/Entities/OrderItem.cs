using GoPress.Domain.Common;

namespace GoPress.Domain.Entities
{
    public class OrderItem:BaseEntity
    {
        public int OrderId { get; set; }

        public int ClothTypeId { get; set; }
        public string ClothName { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal TotalPrice { get; set; }

        public Order Order { get; set; }

        public ClothType ClothType { get; set; }

    }
}
