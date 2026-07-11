namespace GoPress.Mvc.Areas.Customer.Models
{
    public class CustomerRecentOrderViewModel
    {
        public int OrderId { get; set; }

        public string ShopName { get; set; }

        public string Status { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal Amount { get; set; }

        public List<CustomerRecentOrderViewModel> customerRecentOrderViewModels { get; set; } = new();
    }
}
