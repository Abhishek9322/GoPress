namespace GoPress.Mvc.Areas.Customer.Models
{
    public enum OrderStatusEnum
    {
        Pending = 1,
        Accepted = 2,
        PickupAssigned = 3,
        PickedUp = 4,
        Processing = 5,
        ReadyForDelivery = 6,
        OutForDelivery = 7,
        Delivered = 8,
        Cancelled = 9,
        Rejected = 10
    }
}
