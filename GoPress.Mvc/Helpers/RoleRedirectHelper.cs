namespace GoPress.Mvc.Helpers
{
      
        public static class RoleRedirectHelper
        {
            public static (string Action, string Controller, string Area)
                GetRedirect(string role)
            {
                return role switch
                {
                    "Customer"
                        => ("Dashboard", "Dashboard", "Customer"),

                    "ShopOwner"
                        => ("Dashboard", "Dashboard", "ShopOwner"),

                    "DeliveryBoy"
                        => ("Dashboard", "Dashboard", "DeliveryBoy"),

                    "Admin"
                        => ("Admin", "Dashboard", "Dashboard"),

                    _
                        => ("Index", "Home", "")
                };
            }
        
    }
}
