namespace InventoryManagemenSystem_Ims.Entities
{
    public class ReturnGoods:BaseEntity
    {
        public int SalesId { get; set; }
        
        public Sales Sales { get; set; }
        
        public int CustomerId { get; set; }
        
        public Customer Customer { get; set; }
        
        public int SalesManagerId { get; set; }
        
        public SalesManager SalesManager { get; set; }
        
        public string ReturnType { get; set; }
        
        public string Description { get; set; }
        
        public int QuantityReturned { get; set; }
        
    }
}