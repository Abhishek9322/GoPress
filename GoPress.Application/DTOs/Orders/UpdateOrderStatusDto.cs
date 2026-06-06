using GoPress.Domain.Enums;

namespace GoPress.Application.DTOs.Orders
{
    public class UpdateOrderStatusDto
    {
        public OrderStatusEnum Status { get; set; }
    }
}
