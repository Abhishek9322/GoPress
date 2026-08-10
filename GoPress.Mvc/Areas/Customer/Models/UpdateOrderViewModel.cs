namespace GoPress.Mvc.Areas.Customer.Models
{
   
        public class UpdateOrderViewModel
        {
            public int OrderId { get; set; }

            public string PickupAddress { get; set; } = string.Empty;

            public string DeliveryAddress { get; set; } = string.Empty;

            public DateTime PickupDate { get; set; }

            public string? Notes { get; set; }

            public List<UpdateOrderItemViewModel> OrderItems { get; set; }
                = new();
        }
    
     
}
