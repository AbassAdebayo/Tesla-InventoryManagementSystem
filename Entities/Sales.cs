using System.Collections.Generic;

namespace InventoryManagemenSystem_Ims.Entities
{
    public class Sales: BaseEntity
    {
        public int CustomerId { get; set; }
        
        public int SalesManagerId { get; set; }
        
        public SalesManager SalesManager { get; set; }
        
        public Customer Customer { get; set; }
        
        public string CustomerEmailAddress { get; set; }
        
        public string Description { get; set; }

        public ICollection<SalesItem> SalesItems { get; set; } = new List<SalesItem>();
        
        public decimal TotalPrice { get; set; }
    }
}