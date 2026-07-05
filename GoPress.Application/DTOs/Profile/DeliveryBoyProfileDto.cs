using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.DTOs.Profile
{
    public class DeliveryBoyProfileDto 
    {
        public string FullName { get; set; }
        public string Email { get; set; } 
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Pincode { get; set; }

        public string BikeNumber { get; set; }

        public string LicenseNumber { get; set; }

        public string AadhaarNumber { get; set; }
    }
}
