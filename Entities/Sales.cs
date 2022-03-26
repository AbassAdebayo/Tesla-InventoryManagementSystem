using System.Collections.Generic;

namespace InventoryManagemenSystem_Ims.Entities
{
    public class Sales: BaseEntity
    {
        public int ItemId { get; set; }
        
        public Item Item { get; set; }
        public int CustomerId { get; set; }
        
        public Customer Customer { get; set; }
        
        public int SalesManagerId { get; set; }
        
        public SalesManager SalesManager { get; set; }
        
        public int Quantity { get; set; }
              
        public decimal PricePerUnit { get; set; }
        
        public string Description { get; set; }

        public ICollection<SalesItem> SalesItems { get; set; } = new List<SalesItem>();
    }
}