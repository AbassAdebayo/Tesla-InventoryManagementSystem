﻿using System.Collections.Generic;

namespace InventoryManagemenSystem_Ims.Entities
{
    public class Role: BaseEntity
    {
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
