using System.Collections.Generic;

namespace InventoryManagemenSystem_Ims.Entities
{
    public class SalesItem: BaseEntity
    {
        public int SalesId { get; set; }
        
        public Sales Sales { get; set; }
        
        public int ItemId { get; set; }
        
        public Item Item { get; set; }
        
        public int Quantity { get; set; }
        
        public decimal PricePerUnit { get; set; }
        
    }
}