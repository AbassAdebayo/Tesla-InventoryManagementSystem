namespace InventoryManagemenSystem_Ims.Entities
{
    public class AllocateSalesItemToSalesManager:BaseEntity
    {
        public int StockKeeperId { get; set; }
        
        public StockKeeper StockKeeper { get; set; }
        
        public int SalesManagerId { get; set; }
        
        public SalesManager SalesManager { get; set; }
        
        public int QuantityAllocated { get; set; }
        
        public int ItemId { get; set; }
        
        public Item Item { get; set; }
        
    }
}