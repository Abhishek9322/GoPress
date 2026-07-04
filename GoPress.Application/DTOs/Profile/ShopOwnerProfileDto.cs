using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.DTOs.Profile
{
    public class ShopOwnerProfileDto
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string ShopName { get; set; }

        public string ShopAddress { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Pincode { get; set; }

        public string ShopLicenseNumber { get; set; }

        public string GSTNumber { get; set; }
    }
}
