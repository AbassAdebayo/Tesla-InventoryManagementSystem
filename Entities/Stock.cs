using System.Collections.Generic;

namespace InventoryManagemenSystem_Ims.Entities
{
    public class Stock: BaseEntity
    {
        public string StockName { get; set; }
        
        public string Description { get; set; }
        
        public int SupplierId { get; set; }
        
        public Supplier Supplier { get; set; }
        
        public ICollection<StockItem> StockItems { get; set; } = new List<StockItem>();
        
        
    }
}