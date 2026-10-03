namespace GoPress.Mvc.Areas.Admin.Models
{
    public class AllCustomerProfileApiModel
    {
        public int UserId { get; set; }

        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Pincode { get; set; }

        public AllApplicationUserProfileApiModel?
            AllApplicationUserProfileDto
        { get; set; }
    }
}
