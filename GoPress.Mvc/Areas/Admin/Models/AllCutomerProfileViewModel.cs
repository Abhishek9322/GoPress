namespace GoPress.Mvc.Areas.Admin.Models
{
    public class AllCutomerProfileViewModel
    {
        public int UserId { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Pincode { get; set; }

        public AllApplicationUserProfileViewModel? AllApplicationUserProfileViewModel { get; set; }
    }
}
