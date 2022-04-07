using System.Collections.Generic;

namespace InventoryManagemenSystem_Ims.Entities
{
    public class SalesItem: BaseEntity
    {
        public int SalesId { get; set; }
        
        public Sales Sales { get; set; }
        
        
        
    }
}