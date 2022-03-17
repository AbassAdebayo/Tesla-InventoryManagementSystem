using System;
using System.Collections.Generic;

namespace InventoryManagemenSystem_Ims.Entities
{
    public class User:BaseEntity
    {
        public string UserName { get; set; }
        
        public string Email { get; set; }
        
         public string Password { get; set; }
        
         public SalesManager SalesManager { get; set; }
         
         public StockKeeper StockKeeper { get; set; }
         
         public ShopManager ShopManager { get; set; }
         
         public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}