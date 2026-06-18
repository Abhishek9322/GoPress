namespace GoPress.Application.DTOs.Orders
{
    public class ShopOrderDto
    {
        public int OrderId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerPhone { get; set; }

        public string PickupAddress { get; set; }

        public string DeliveryAddress { get; set; }

        public decimal TotalAmount { get; set; }

        public string Status { get; set; }
    }
}
