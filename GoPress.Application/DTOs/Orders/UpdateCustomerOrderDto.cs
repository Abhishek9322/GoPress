namespace GoPress.Application.DTOs.Orders
{
    public class UpdateCustomerOrderDto
    {
        public string PickupAddress { get; set; }

        public string DeliveryAddress { get; set; }

        public DateTime PickupDate { get; set; }

        public string? Notes { get; set; }

        public List<UpdateCustomerOrderItemDto> OrderItems { get; set; }
    }
}
