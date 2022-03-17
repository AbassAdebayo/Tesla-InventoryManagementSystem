namespace InventoryManagemenSystem_Ims.Entities
{
    public class Payment:BaseEntity
    {
        public int PaymentReference { get; set; }
        
        public int salesId { get; set; }
        
        public Sales Sales { get; set; }
        
        public int CustomerId { get; set; }
        
        public Customer Customer { get; set; }
        
        public decimal Amount { get; set; }
        
        public decimal Discount { get; set; }
    }
}