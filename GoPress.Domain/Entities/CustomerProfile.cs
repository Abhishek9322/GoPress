using GoPress.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Domain.Entities
{
    public class CustomerProfile:BaseEntity
    {
        public int UserId { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Pincode { get; set; }

        // Navigation Property
        public ApplicationUser User { get; set; }
    }
}
