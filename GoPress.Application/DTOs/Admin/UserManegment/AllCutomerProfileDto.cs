using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.DTOs.Admin.UserManegment
{
    public class AllCutomerProfileDto
    {
        public int UserId { get; set; } 

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Pincode { get; set; }

        public AllApplicationUserProfileDto? AllApplicationUserProfileDto { get; set; }
    }
}
