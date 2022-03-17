using System.Collections.Generic;
using InventoryManagemenSystem_Ims.Entities;

namespace InventoryManagemenSystem_Ims.DTOs
{
    public class RoleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }

    public class CreateRoleRequestModel
    {
        public string Name { get; set; }
        
        public string Description { get; set; }

    }
    
    public class UpdateRoleRequestModel
    {
        public string Name { get; set; }
        
        public string Description { get; set; }

    }
}