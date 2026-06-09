namespace GoPress.Application.DTOs.Orders
{
    public class AvailableOrderDto
    {
        public int OrderId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerPhone { get; set; }

        public string PickupAddress { get; set; }

        public string ShopName { get; set; }

        public string ShopAddress { get; set; }

        public DateTime PickupDate { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
