using GoPress.Domain.Enums;

namespace GoPress.Application.DTOs.Admin.UserManegment
{
    public class AllApplicationUserProfileDto
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    
        public UserRoleenum Role { get; set; }
      
    }
}
