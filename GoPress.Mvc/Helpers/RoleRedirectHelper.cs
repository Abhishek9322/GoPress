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
                        => ("Customer", "Dashboard", "Dashboard"),

                    "ShopOwner"
                        => ("ShopOwner", "Dashboard", "Dashboard"),

                    "DeliveryBoy"
                        => ("DeliveryBoy", "Dashboard", "Dashboard"),

                    "Admin"
                        => ("Admin", "Dashboard", "Dashboard"),

                    _
                        => ("Index", "Home", "")
                };
            }
        
    }
}
