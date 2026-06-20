using GoPress.Domain.Common;
using GoPress.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Domain.Entities
{
    public class ApplicationUser:BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? Password { get; set; }
        public string PasswordHash { get; set; }
        public UserRoleenum Role { get; set; }
        public bool IsApproved { get; set; } = false;
        public bool IsActive { get; set; } = false;

        // Navigation Properties
        public CustomerProfile CustomerProfile { get; set; }

        public DeliveryBoyProfile DeliveryBoyProfile { get; set; }

        public ShopOwnerProfile ShopOwnerProfile { get; set; }

        public ICollection<RefreshToken> RefreshTokens { get; set; }

        //order 
        public ICollection<Order> CustomerOrders { get; set; }

        public ICollection<Order> ShopOwnerOrders { get; set; }

        public ICollection<Order> DeliveryBoyOrders { get; set; }
        public ICollection<ShopOwnerClothPrice>ShopOwnerClothPrices{ get; set; }
    }
}
